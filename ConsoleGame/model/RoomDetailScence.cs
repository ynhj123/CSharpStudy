using ConsoleGame.Controller;
using System;
using System.Threading;

namespace ConsoleGame.model
{
    [GameCommon.Ioc.Annotation.Component]
    class RoomDetailScence : Scence
    {
        Room room;


        public Room Room { get => room; set => room = value; }

        public void Handle()
        {
            NetManagerEvent.Update();
            Print();
            OnKeyUp();
            Thread.Sleep(1000);

        }

        private void OnKeyUp()
        {
            char key = 'z';
            while (Console.KeyAvailable)
            {
                key = Console.ReadKey(true).KeyChar;

            }
            HandleKey(key);
        }

        private void HandleKey(char key)
        {
            if ('1' == key)
            {
                User user = ScenceController.user;
                if (SwitchOwn(user.Userid))
                {
                    //开始游戏
                    MsgStartBattle msgStartBattle = new MsgStartBattle();
                    NetManagerEvent.Send(msgStartBattle);
                }
                else
                {
                    if (room.UserStatus[user.Userid])
                    {
                        //取消准备
                        MsgUnprepare msgUnprepare = new MsgUnprepare();
                        NetManagerEvent.Send(msgUnprepare);
                    }
                    else
                    {
                        //准备
                        MsgPrepare msgPrepare = new MsgPrepare();
                        NetManagerEvent.Send(msgPrepare);
                    }
                }
            }
            else if ('2' == key)
            {
                MsgLeaveRoom msgLeaveRoom = new MsgLeaveRoom();
                NetManagerEvent.Send(msgLeaveRoom);
                ScenceController.curScence = ScenceController.scenceDict["room"];
            }
        }

        private void Print()
        {
            Console.Clear();
            System.Collections.Generic.List<User> users = Room.Users;
            System.Collections.Generic.Dictionary<string, bool> userStatus = Room.UserStatus;

            Console.WriteLine(@" 序号 |  用户名  |  积分  |   状态 |");
            for (int i = 0; i < users.Count; i++)
            {
                User user = users[i];
                string status = "";
                if (SwitchOwn(user.Userid))
                {
                    status = "房主";
                }
                else
                {
                    status = userStatus[user.Userid] == true ? "准备" : "待准备";
                }
                SwitchColor(i);

                Console.WriteLine("   {0}  |  {1}|{2}| {3} |", i + 1, user.Username.PadLeft(8,' '), Convert.ToInt16(user.Score).ToString().PadLeft(8, ' '), status.PadLeft(4, ' '));

            }
            Console.ResetColor();
            PrintKey();
        }

        private void PrintKey()
        {
            User user = ScenceController.user;
            bool isOwn = SwitchOwn(user.Userid);
            Console.WriteLine("请输入: ");
            if (isOwn)
            {
                Console.WriteLine("1: 开始游戏");
            }
            else
            {
                if (room.UserStatus[user.Userid])
                {
                    Console.WriteLine("1: 取消准备");
                }
                else
                {
                    Console.WriteLine("1: 准备");
                }

            }
            Console.WriteLine("2: 退出房间");
            if (isOwn)
            {
                Console.WriteLine("3: 剔除玩家");
            }

        }

        private bool SwitchOwn(string userid)
        {
            return room.OwnId == userid;
        }
        private void SwitchColor(int i)
        {
            switch (i)
            {
                case 0:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case 1:
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case 3:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case 4:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case 5:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case 6:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case 7:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case 8:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
        }
    }
}
