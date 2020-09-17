using GameServer.script.net;
using System;

namespace GameServer.script.logic
{
    class BattleMsgHandler
    {
    }
    public partial class MsgHandler
    {
        public static void MsgMove(ClientState c, MsgBase msg)
        {
            MsgMove msgMove = (MsgMove)msg;
            Console.WriteLine(msgMove.x);
            msgMove.x++;
            NetManager.Send(c, msgMove);
        }
    }
}
