using System;

namespace ObjectOriented
{
    class Program
    {
        //是否进行
        static bool isStart = true;
        static void Main(string[] args)
        {

            Scence scence = new Scence(20, 20);
            player player = new player(scence, 1, 1);
            //是否刷新
            bool isFlush = true;

            while (isStart)
            {
                if (isFlush)
                {
                    scence.Print(player);
                    Console.WriteLine("游戏开始，请输入w，a，s，d移动,按q退出");
                }
                char keyChar = Console.ReadKey().KeyChar;
                if (keyChar == 'q')
                {
                    isStart = false;
                }
                else
                {
                    isFlush = player.move(keyChar);

                }
            }



        }


    }
}
