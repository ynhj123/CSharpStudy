using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace echoSelect
{
    class Program
    {
        public static Dictionary<Socket, ClientState> clients = new Dictionary<Socket, ClientState>();
        static Socket listenSocket;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress iPAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, 8888);
            listenSocket.Bind(iPEndPoint);
            listenSocket.Listen(0);
            Console.WriteLine("服务器启动");
            List<Socket> checkRead = new List<Socket>();
            while (true)
            {
                checkRead.Add(listenSocket);
                foreach (ClientState client in clients.Values)
                {
                    checkRead.Add(client.socket);
                }
                Socket.Select(checkRead, null, null, 1000);
                foreach (Socket socket in checkRead)
                {
                    if (socket == listenSocket)
                    {
                        ReadListenSocket(socket);
                    }
                    else
                    {
                        ReadClientSocket(socket);
                    }
                }
            }
        }
        public static void ReadListenSocket(Socket socket)
        {
            Console.WriteLine("Accept");
            Socket clientSokcet = listenSocket.Accept();
            ClientState clientState = new ClientState();
            clientState.socket = clientSokcet;
            clients.Add(clientSokcet, clientState);
        }

        public static object ReadClientSocket(Socket clientSocket)
        {
            ClientState clientState = clients[clientSocket];
            int count = 0;
            try
            {
                count = clientSocket.Receive(clientState.readBuff);

            }
            catch (SocketException ex)
            {

                System.Reflection.MethodInfo methodInfo = typeof(EventHandle).GetMethod("OnDisconnect");
                object[] ob = new object[] { clientState };
                methodInfo.Invoke(null, ob);

                clientSocket.Close();
                clients.Remove(clientSocket);
                Console.WriteLine("Receive SocketException" + ex.ToString());

                return false;
            }
            if (count <= 0)
            {
                System.Reflection.MethodInfo methodInfo = typeof(EventHandle).GetMethod("OnDisconnect");
                object[] ob = new object[] { clientState };
                methodInfo.Invoke(null, ob);

                clientSocket.Close();
                clients.Remove(clientSocket);
                Console.WriteLine("Socket Close");
                return false;
            }
            string recvStr = System.Text.Encoding.Default.GetString(clientState.readBuff, 0, count);
            Console.WriteLine("Receive" + recvStr);
            string[] split = recvStr.Split('|');
            System.Reflection.MethodInfo msgMethod = typeof(MsgHandle).GetMethod("Msg" + split[0]);
            object[] msgOb = new object[] { clientState, split[1] };
            msgMethod.Invoke(null, msgOb);



            //string sengStr = clientSocket.RemoteEndPoint.ToString() + ":" + recvStr;
            /* byte[] sendBytes = System.Text.Encoding.Default.GetBytes(recvStr);
             foreach (ClientState state in clients.Values)
             {
                 state.socket.Send(sendBytes);
             }*/



            return true;

        }
    }
    public class ClientState
    {
        public Socket socket;
        public byte[] readBuff = new byte[1024];

        public int hp = -100;
        public float x = 0;
        public float y = 0;
        public float z = 0;
        public float eulY = 0;


    }
}
