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
        private Dictionary<string, bool> userStatus = new Dictionary<string, bool>();

        private List<User> users = new List<User>();
        public int RoomId { get => roomId; set => roomId = value; }
        public string OwnId { get => ownId; set => ownId = value; }
        public int PlayerCount { get => playerCount; set => playerCount = value; }
        public int MaxCount { get => maxCount; set => maxCount = value; }
        public int Status { get => status; set => status = value; }
        public List<User> Users { get => users; set => users = value; }
        public Dictionary<string, bool> UserStatus { get => userStatus; set => userStatus = value; }

        internal bool AddUser(string id)
        {
            if (playerCount == maxCount || status == 1)
            {
                return false;
            }
            if (UserStatus.Count == maxCount)
            {
                return false;

            }
            UserStatus.Add(id, false);

            ownId = UserStatus.Keys.First();
            return true;
        }

        public bool RemoveUser(string id)
        {
            //获取玩家
            Player player = PlayerManager.GetPlayer(id);
            if (player == null)
            {
                Console.WriteLine("room.RemovePlayer fail, player is null");
                return false;
            }
            //没有在房间里
            if (!UserStatus.ContainsKey(id))
            {
                Console.WriteLine("room.RemovePlayer fail, not in this room");
                return false;
            }
            //删除列表
            UserStatus.Remove(id);
            //
            User user = users.Where(user => user.Userid == id).FirstOrDefault();
            users.Remove(user);
            playerCount--;
            //设置房主
            if (UserStatus.Count == 0)
            {
                ownId = "";
            }
            else
            {
                ownId = UserStatus.Keys.First();

            }
            //战斗状态退出
            if (status == 1)
            {

                /*MsgLeaveBattle msg = new MsgLeaveBattle();
                msg.id = player.id;
                Broadcast(msg);*/
            }

            return true;
        }

        internal bool AddUser(User user)
        {
            if (playerCount == maxCount || status == 1)
            {
                return false;
            }
            if (UserStatus.Count >= maxCount)
            {
                return false;

            }
            //准备状态才能加人
            if (status != 0)
            {
                Console.WriteLine("room.AddPlayer fail, not PREPARE");
                return false;
            }
            //已经在房间里
            if (userStatus.ContainsKey(user.Userid))
            {
                Console.WriteLine("room.AddPlayer fail, already in this room");
                return false;
            }
            UserStatus.Add(user.Userid, false);
            playerCount++;
            ownId = UserStatus.Keys.First();
            user.RoomId = roomId;
            users.Add(user);
            ownId = UserStatus.Keys.First();
            return true;
        }

        internal bool Unprepare(User user)
        {
            UserStatus[user.Userid] = false;
            return true;
        }

        internal bool Prepare(User user)
        {
            UserStatus[user.Userid] = true;
            return true;
        }

        public void Broadcast(MsgBase msg)
        {
            foreach (string playerId in UserStatus.Keys)
            {
                Player player = PlayerManager.GetPlayer(playerId);
                player.Send(msg);
            }
        }

        internal bool IsOwner(User user)
        {
            return ownId == user.Userid;
        }

        internal bool StartBattle()
        {
            if (userStatus.Count == 1)
            {
                return false;
            }
            int count = userStatus.Where(pair => pair.Key != ownId && pair.Value == false).Count();
            return count == 0;
        }
    }
}
