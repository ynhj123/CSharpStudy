using GameServer.script.net;

namespace GameServer.script.logic
{
    class BattleMsgHandler
    {
    }
    public partial class MsgHandler
    {
        public static void MsgMove(ClientState c, MsgBase msg)
        {


            NetManager.Send(c, msg);
        }
    }
}
