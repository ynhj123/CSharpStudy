using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace echoSelect
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
                clientSocket.Close();
                clients.Remove(clientSocket);
                Console.WriteLine("Receive SocketException" + ex.ToString());
                return false;
            }
            if (count == 0)
            {
                clientSocket.Close();
                clients.Remove(clientSocket);
                Console.WriteLine("Socket Close");
                return false;
            }
            string recvStr = System.Text.Encoding.Default.GetString(clientState.readBuff, 0, count);
            Console.WriteLine("Receive" + recvStr);
            string sengStr = clientSocket.RemoteEndPoint.ToString() + ":" + recvStr;
            byte[] sendBytes = System.Text.Encoding.Default.GetBytes(sengStr);
            foreach (ClientState state in clients.Values)
            {
                state.socket.Send(sendBytes);
            }
            return true;

        }
    }
    class ClientState
    {
        public Socket socket;
        public byte[] readBuff = new byte[1024];
    }
}
