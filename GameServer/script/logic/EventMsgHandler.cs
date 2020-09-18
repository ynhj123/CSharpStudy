using GameServer.script.net;
using System;

namespace GameServer.script
{
    class EventMsgHandler
    {
    }
    public partial class EventHandler
    {
        public static void OnDisconnect(ClientState c)
        {
            Console.WriteLine("close");

        }
        public static void OnTimer()
        {
            checkPing();
        }
        public static void checkPing()
        {
            long timeNow = NetManager.GetTimeStamp();
            foreach (ClientState s in NetManager.clients.Values)
            {
                if (timeNow - s.lastPingTime > NetManager.pingInterval * 4)
                {
                    Console.WriteLine("ping close {0}", s.socket.RemoteEndPoint.ToString());
                    NetManager.Close(s);
                    return;
                }
            }
        }
    }
}
