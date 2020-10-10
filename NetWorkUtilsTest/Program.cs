using NetWorkUtils.Client;
using System;
using System.Threading;

namespace NetWorkUtilsTest
{
    class Program
    {
        static void Main(string[] args)
        {
            NetUtils.Start("127.0.0.1", 8888);
            Thread.Sleep(1000);
            NetUtils.Enter("test", "test");
            while (true)
            {
                MsgGetRoomList msgGetRoomList = new MsgGetRoomList();
                NetUtils.Send(msgGetRoomList);
                NetUtils.Update();
                Thread.Sleep(20);
                
                
            }
        }
    }
}
