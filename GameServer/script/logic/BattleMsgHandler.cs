using GameServer.script.net;
using System.Collections.Generic;

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
            Player player = PlayerManager.GetPlayer(msgMove.spriteId);
            player.x = msgMove.x;
            player.y = msgMove.y;
            player.veloctity = msgMove.veloctity;
            PlayerManager.Broadcast(msg);
        }
        public static void MsgAttack(ClientState c, MsgBase msg)
        {

            PlayerManager.Broadcast(msg);
        }

        public static void MsgEnter(ClientState c, MsgBase msg)
        {
            MsgEnter msgEnter = (MsgEnter)msg;
            Player player = new Player(c);
            player.hp = msgEnter.hp;
            player.id = msgEnter.playId;
            player.style = msgEnter.style;
            player.x = msgEnter.x;
            player.y = msgEnter.y;
            player.veloctity = msgEnter.veloctity;
            PlayerManager.AddPlayer(player);
            List<MsgEnter> lists = new List<MsgEnter>();
            foreach (var item in PlayerManager.players.Values)
            {
                MsgEnter newMsg = new MsgEnter();
                newMsg.hp = item.hp;
                newMsg.playId = item.id;
                newMsg.style = item.style;
                newMsg.x = item.x;
                newMsg.y = item.y;
                newMsg.veloctity = item.veloctity;
                lists.Add(newMsg);
            }
            msgEnter.players = lists;
            PlayerManager.Broadcast(msgEnter);
        }
        public static void OnLeave(ClientState c, MsgBase msg)
        {
            NetManager.Send(c, msg);
        }
    }
}
