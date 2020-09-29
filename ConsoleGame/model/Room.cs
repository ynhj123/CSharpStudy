using System;
using System.Collections.Generic;

namespace ConsoleGame.model
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
        public int PlayerCount { get => playerCount; set => playerCount = value; }
        public int Status { get => status; set => status = value; }
        public string OwnId { get => ownId; set => ownId = value; }
        public int MaxCount { get => maxCount; set => maxCount = value; }
        public Dictionary<string, bool> UserStatus { get => userStatus; set => userStatus = value; }
        public List<User> Users { get => users; set => users = value; }

        public void Print()
        {

            if (status == 0)
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.WriteLine("|id:{0}  | {1}/{2} |", roomId.ToString().PadLeft(6, '0'), playerCount, MaxCount);
            Console.ResetColor();
        }

    }
}
