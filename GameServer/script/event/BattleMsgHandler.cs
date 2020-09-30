using GameServer.script.net;
using System.Collections.Generic;

namespace GameServer.script.logic
{
    class BattleMsgHandler
    {
    }
    public partial class MsgHandler
    {

        public static void MsgMove(ClientState c, MsgBase msgBase)
        {
            MsgMove msg = (MsgMove)msgBase;
            Player player = PlayerManager.GetPlayer(msg.spriteId);
            player.x = msg.x;
            player.y = msg.y;
            player.veloctity = msg.veloctity;

            User user = c.user;

            //获取房间
            Room room = RoomManager.GetRoom(user.RoomId);
            room.Broadcast(msg);
        }
        public static void MsgAttack(ClientState c, MsgBase msg)
        {

            User user = c.user;

            //获取房间
            Room room = RoomManager.GetRoom(user.RoomId);
            room.Broadcast(msg);
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
            User user = c.user;

            //获取房间
            Room room = RoomManager.GetRoom(user.RoomId);
            room.Broadcast(msgEnter);
        }
        public static void MsgLeave(ClientState c, MsgBase msg)
        {

            User user = c.user;

            //获取房间
            Room room = RoomManager.GetRoom(user.RoomId);
            room.Broadcast(msg);
        }
    }
}
