using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace EchoAsynchronous
{
    class Program
    {
        static Dictionary<Socket, ClientState> clients = new Dictionary<Socket, ClientState>();
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
            listenSocket.BeginAccept(AcceptCallback, listenSocket);
            Console.ReadKey();
        }
        public static void AcceptCallback(IAsyncResult result)
        {
            try
            {
                Console.WriteLine("服务器Accept");
                Socket socket = (Socket)result.AsyncState;
                Socket clientSocket = socket.EndAccept(result);
                ClientState clientState = new ClientState();
                clientState.socket = clientSocket;
                clients.Add(clientSocket, clientState);
                clientSocket.BeginReceive(clientState.readBuff, 0, 1024, 0, ReceiveCallBack, clientState);
                socket.BeginAccept(AcceptCallback, socket);
            }
            catch (SocketException ex)
            {
                Console.WriteLine("Socket accept faild,reason" + ex.ToString());
            }
        }

        private static void ReceiveCallBack(IAsyncResult result)
        {
            try
            {
                ClientState state = (ClientState)result.AsyncState;
                Socket socket = state.socket;
                int count = socket.EndReceive(result);
                //关闭客户端
                if (count == 0)
                {
                    socket.Close();
                    clients.Remove(socket);
                    Console.WriteLine("Socket close");
                    return;
                }
                string recvStr = System.Text.Encoding.Default.GetString(state.readBuff, 0, count);
                byte[] sendBytes = System.Text.Encoding.Default.GetBytes("echo" + recvStr);
                socket.Send(sendBytes);//同步 or异步
                socket.BeginReceive(state.readBuff, 0, 1024, 0, ReceiveCallBack, state);

            }
            catch (SocketException ex)
            {
                Console.WriteLine("Socket receive faild,reason" + ex.ToString());
            }
        }
    }
    class ClientState
    {
        public Socket socket;
        public byte[] readBuff = new byte[1024];
    }
}
