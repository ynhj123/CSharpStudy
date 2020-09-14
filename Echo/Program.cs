using System;
using System.Net;
using System.Net.Sockets;

namespace Echo
{
    class Program
    {
        static void Main(string[] args)
        {

            Socket listenServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress iPAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, 8888);
            listenServer.Bind(iPEndPoint);
            listenServer.Listen(0);
            Console.WriteLine("服务器启动");
            while (true)
            {
                Socket socket = listenServer.Accept();
                Console.WriteLine("服务器启动 accept");
                byte[] readBuff = new byte[1024];
                int count = socket.Receive(readBuff);
                string readStr = System.Text.Encoding.Default.GetString(readBuff, 0, count);
                string sendStr = "你好,服务器接收内容：[" + readStr + "]";
                byte[] sendBytes = System.Text.Encoding.Default.GetBytes(sendStr);

                socket.Send(sendBytes);
            }


        }
    }
}
