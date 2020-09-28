using System;
using System.Collections.Generic;

namespace ConsoleGame.model
{
    class Room
    {
        private int roomId;
        private string ownId; //房主id
        private int playerCount;//当前玩家数
        private int maxCount; //最大玩家数
        private int status; // 0  待开始 1进行中
        private List<Player> players = new List<Player>();

        public int RoomId { get => roomId; set => roomId = value; }
        public int PlayerCount { get => playerCount; set => playerCount = value; }
        public int Status { get => status; set => status = value; }
        public string OwnId { get => ownId; set => ownId = value; }
        public int MaxCount { get => maxCount; set => maxCount = value; }
        public List<Player> Players { get => players; set => players = value; }

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
