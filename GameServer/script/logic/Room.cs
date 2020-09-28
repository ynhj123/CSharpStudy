using System;
using System.Collections.Generic;
using System.Linq;

namespace GameServer.script.logic
{
    public class Room
    {
        private int roomId;
        private string ownId; //房主id
        private int playerCount;//当前玩家数
        private int maxCount; //最大玩家数
        private int status; // 0  待开始 1进行中
        public Dictionary<string, bool> playerIds = new Dictionary<string, bool>();

        public int RoomId { get => roomId; set => roomId = value; }
        public string OwnId { get => ownId; set => ownId = value; }
        public int PlayerCount { get => playerCount; set => playerCount = value; }
        public int MaxCount { get => maxCount; set => maxCount = value; }
        public int Status { get => status; set => status = value; }

        internal bool AddPlayer(string id)
        {
            if (playerCount == maxCount || status == 1)
            {
                return false;
            }
            lock (playerIds)
            {
                if(playerIds.Count == maxCount)
                {
                    return false;
                   
                }
                playerIds.Add(id, false);
               
            }
      
            ownId = playerIds.Keys.First();
            return true;
        }

        public bool RemovePlayer(string id)
        {
            //获取玩家
            Player player = PlayerManager.GetPlayer(id);
            if (player == null)
            {
                Console.WriteLine("room.RemovePlayer fail, player is null");
                return false;
            }
            //没有在房间里
            if (!playerIds.ContainsKey(id))
            {
                Console.WriteLine("room.RemovePlayer fail, not in this room");
                return false;
            }
            //删除列表
            playerIds.Remove(id);
            //设置玩家数据
        
            player.roomId = -1;
            //设置房主
            ownId = playerIds.Keys.First();
            //战斗状态退出
            if (status == 1)
            {
             
                /*MsgLeaveBattle msg = new MsgLeaveBattle();
                msg.id = player.id;
                Broadcast(msg);*/
            }
            
            return true;
        }

        private void Broadcast(MsgBase msg)
        {
            foreach (string playerId in playerIds.Keys)
            {
                Player player = PlayerManager.GetPlayer(playerId);
                player.Send(msg);
            }
        }
    }
}
