using GameServer.script.logic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace GameServer.script.net
{
    public class NetManager
    {
        public static Socket listenfd;
        public static Dictionary<Socket, ClientState> clients = new Dictionary<Socket, ClientState>();
        static List<Socket> checkRead = new List<Socket>();
        public static long pingInterval = 5;
        public static void StartLoop(int listenPort)
        {
            listenfd = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress iPAddress = IPAddress.Parse("0.0.0.0");
            IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, listenPort);
            listenfd.Bind(iPEndPoint);
            listenfd.Listen(0);
            Console.WriteLine("[服务器]启动成功 prot:{0}", listenPort);
            while (true)
            {
                ResetCheckRead(); // 重置
                Socket.Select(checkRead, null, null, 1000);
                for (int i = checkRead.Count - 1; i >= 0; i--)
                {
                    Socket s = checkRead[i];
                    if (s == listenfd)
                    {
                        ReadListenfd(s);
                    }
                    else
                    {
                        ReadClientfd(s);
                    }
                }
                //如果是连接请求
                Timer();
            }
        }

        private static void Timer()
        {
            System.Reflection.MethodInfo methodInfo = typeof(EventHandler).GetMethod("OnTimer");
            object[] ob = { };
            methodInfo.Invoke(null, ob);
        }

        private static void ReadClientfd(Socket clientfd)
        {
            ClientState state = clients[clientfd];
            ByteArray readBuff = state.readBuff;
            int count = 0;
            if (readBuff.remain <= 0)
            {
                OnReceiveData(state);
                readBuff.MoveBytes();
            }
            if (readBuff.remain <= 0)
            {
                Debug.WriteLine("Receive fail,naybe msg length > buff capacity");
                Close(state);
                return;
            }
            try
            {
                count = clientfd.Receive(readBuff.bytes, readBuff.writeIdx, readBuff.remain, 0);

            }
            catch (SocketException ex)
            {
                Debug.WriteLine("Socket close {0} reason {1}", clientfd.RemoteEndPoint.ToString(), ex.ToString());
                Close(state);
                return;
            }
            readBuff.writeIdx += count;
            OnReceiveData(state);
            readBuff.CheckAndMoveBytes();
        }

        private static void OnReceiveData(ClientState state)
        {
            ByteArray readBuff = state.readBuff;
            //msglength
            if (readBuff.length <= 2)
            {
                return;
            }
            Int16 bodyLength = readBuff.ReadInt16();
            //msg
            if (readBuff.length < bodyLength)
            {
                return;
            }
            //name
            int nameCount = 0;
            string protoName = MsgBase.DecodeName(readBuff.bytes, readBuff.readIdx, out nameCount);
            if (protoName == "")
            {
                Debug.WriteLine("OnReceiveData msgDecodeName failed");
                Close(state);
            }
            readBuff.readIdx += nameCount;

            //body
            int bodyCount = bodyLength - nameCount;
            MsgBase msg = MsgBase.Decode<MsgBase>(protoName, readBuff.bytes, readBuff.readIdx, bodyCount);

            readBuff.readIdx += bodyCount;
            readBuff.CheckAndMoveBytes();

            //fire
            System.Reflection.MethodInfo methodInfo = typeof(MsgHandler).GetMethod(protoName);
            object[] ob = { state, msg };
            Debug.WriteLine("Receive {0}", protoName);
            if (methodInfo != null)
            {
                methodInfo.Invoke(null, ob);
            }
            else
            {
                Debug.WriteLine("OnReceiveData invoke fail {0}", protoName);
            }
            //广播
            /*foreach (var item in clients.Values)
            {
                Send(item, msg);
            }*/
            if (readBuff.length > 2)
            {
                OnReceiveData(state);
            }
        }

        public static void Close(ClientState state)
        {
            System.Reflection.MethodInfo methodInfo = typeof(EventHandler).GetMethod("OnDisconnect");
            object[] ob = { state };
            methodInfo.Invoke(null, ob);
            state.socket.Close();
            clients.Remove(state.socket);
        }

        private static void ReadListenfd(Socket s)
        {
            try
            {
                Socket clientfd = listenfd.Accept();
                Debug.WriteLine("accept {0}", clientfd.RemoteEndPoint);
                ClientState clientState = new ClientState();
                clientState.socket = clientfd;
                clientState.lastPingTime = NetManager.GetTimeStamp();
                clients.Add(clientfd, clientState);
            }
            catch (SocketException ex)
            {

                Debug.WriteLine("accept fail {0}", ex.ToString());
            }
        }

        public static void ResetCheckRead()
        {
            checkRead.Clear();
            checkRead.Add(listenfd);
            foreach (ClientState s in clients.Values)
            {
                checkRead.Add(s.socket);
            }
        }
        public static void Send(ClientState cs, MsgBase msg)
        {
            if (cs == null)
            {
                return;
            }
            if (!cs.socket.Connected)
            {
                return;
            }
            byte[] nameBytes = MsgBase.EncodeName(msg);
            byte[] bodyBytes = MsgBase.Encode(msg);
            int len = nameBytes.Length + bodyBytes.Length;
            byte[] sendBytes = new Byte[2 + len];
            sendBytes[0] = (byte)(len % 256);
            sendBytes[1] = (byte)(len / 256);

            Array.Copy(nameBytes, 0, sendBytes, 2, nameBytes.Length);
            Array.Copy(bodyBytes, 0, sendBytes, 2 + nameBytes.Length, bodyBytes.Length);
            try
            {
                cs.socket.BeginSend(sendBytes, 0, sendBytes.Length, 0, null, null);
            }
            catch (SocketException ex)
            {
                Debug.WriteLine("Socket Close on BeginSend {0}", ex.ToString());
                throw;
            }
        }

        public static void Send(ClientState cs, string msg)
        {
            if (cs == null)
            {
                return;
            }
            if (!cs.socket.Connected)
            {
                return;
            }
            MsgBase msgBase = JsonConvert.DeserializeObject<MsgBase>(msg);
            byte[] nameBytes = MsgBase.EncodeName(msgBase);
            byte[] bodyBytes = System.Text.Encoding.UTF8.GetBytes(msg);
            int len = nameBytes.Length + bodyBytes.Length;
            byte[] sendBytes = new Byte[2 + len];
            sendBytes[0] = (byte)(len % 256);
            sendBytes[1] = (byte)(len / 256);

            Array.Copy(nameBytes, 0, sendBytes, 2, nameBytes.Length);
            Array.Copy(bodyBytes, 0, sendBytes, 2 + nameBytes.Length, bodyBytes.Length);
            try
            {
                cs.socket.BeginSend(sendBytes, 0, sendBytes.Length, 0, null, null);
            }
            catch (SocketException ex)
            {
                Debug.WriteLine("Socket Close on BeginSend {0}", ex.ToString());
                throw;
            }
        }

        public static long GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);
        }
    }
}
