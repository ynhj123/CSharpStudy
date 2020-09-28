using System;
using System.Collections.Generic;

namespace ConsoleGame.model
{
    [GameCommon.Ioc.Annotation.Component]
    class RoomScence : Scence
    {
        public static Dictionary<int, Room> rooms = new Dictionary<int, Room>();



        public void Handle()
        {
            Console.Clear();
            if (rooms.Count == 0)
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
                foreach (var item in rooms.Values)
                {
                    item.Print();
                }
            }
            Console.WriteLine(@"
请选择：
1.加入房间
2.刷新列表
");
            char keyChar = Console.ReadKey().KeyChar;
        }
    }
}
