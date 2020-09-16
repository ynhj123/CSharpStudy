using GameServer.script.net;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer.script.logic
{
    class SysMsgHandler
    {
    }
    public partial class MsgHandler
    {
        public static void MsgPing(ClientState c,MsgBase msg)
        {
            Console.WriteLine("MsgPing");
            c.lastPingTime = NetManager.GetTimeStamp();
            MsgPong msgPong = new MsgPong();
            NetManager.Send(c, msgPong);
        }
    } 
}
