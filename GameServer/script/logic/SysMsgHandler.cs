using GameServer.script.net;
using System;

namespace GameServer.script.logic
{
    class SysMsgHandler
    {
    }
    public partial class MsgHandler
    {
        public static void MsgPing(ClientState c, string  msg)
        {
            Console.WriteLine("MsgPing");
            c.lastPingTime = NetManager.GetTimeStamp();
            MsgPong msgPong = new MsgPong();
            NetManager.Send(c, msgPong);
        }
    }
}
