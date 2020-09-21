using GameServer.script.net;
using Newtonsoft.Json;
using System;

namespace GameServer.script.logic
{
    class BattleMsgHandler
    {
    }
    public partial class MsgHandler
    {
        public static void MsgMove(ClientState c, string msg)
        {

            MsgMove msgMove = JsonConvert.DeserializeObject<MsgMove>(msg);
            Console.WriteLine(msgMove.x);
            /*msgMove.x++;*/
            NetManager.Send(c, msgMove);
        }
    }
}
