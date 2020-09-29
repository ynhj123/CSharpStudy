using ConsoleGame.Controller;
using ConsoleGame.utils.Time;
using GameCommon.Builder;
using System;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleGame.model
{
    [GameCommon.Ioc.Annotation.Component]
    public class RoomScence : Scence
    {
        private Dictionary<int, Room> rooms = new Dictionary<int, Room>();
        private bool isEnterRoomCallBack;

        public Dictionary<int, Room> Rooms { get => rooms; set => rooms = value; }
        public bool IsEnterRoomCallBack { get => isEnterRoomCallBack; set => isEnterRoomCallBack = value; }

        public void Handle()
        {

            NetManagerEvent.Update();

            Console.Clear();
            if (Rooms.Count == 0)
            {
                Console.WriteLine("当前没有房间");
            }
            else
            {
                Console.WriteLine(@"
------------------------------------------
|      房间列表（红色为进行中）           |
-------------------------------------------
");
                foreach (var item in Rooms.Values)
                {
                    item.Print();
                }
            }
            Console.WriteLine(@"
请选择：
1.加入房间
2.刷新列表
3.返回
");
            char keyChar = Console.ReadKey().KeyChar;
            HandleKey(keyChar);
        }

        private void HandleKey(char keyChar)
        {
            if ('1' == keyChar)
            {
                bool isSuccess = false;
                int roomId = -1;
                while (!isSuccess)
                {
                    Console.WriteLine("请输入房间id");
                    string str = Console.ReadLine();
                    isSuccess = int.TryParse(str, out roomId);
                }
                MsgEnterRoom msgEnterRoom = new MsgEnterRoom();
                msgEnterRoom.roomId = roomId;
                NetManagerEvent.Send(msgEnterRoom);
                IsEnterRoomCallBack = false;
                TimeEvent.Handle(1, 5, ref isEnterRoomCallBack, () =>
                {
                    NetManagerEvent.Update();
                }, () =>
                {
                    MsgLeaveRoom msg = new MsgLeaveRoom();
                    NetManagerEvent.Send(msgEnterRoom);
                    Console.WriteLine("请求超时，请重试！");
                });
                RoomDetailScence roomDetailScence = ContainerBuilder.Resolve<RoomDetailScence>();
                if (roomDetailScence.Room == null)
                {
                    Thread.Sleep(2000);
                }




            }else if('2' == keyChar)
            {
                ScenceController.curScence = ScenceController.scenceDict["room"];
                MsgListRoom msgListRoom = new MsgListRoom();
                NetManagerEvent.Send(msgListRoom);
            }
            else if ('3' == keyChar)
            {
                ScenceController.curScence = ScenceController.scenceDict["index"];
            }

        }
    }
}
