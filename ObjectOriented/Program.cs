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
            player player2 = new player("永恩", 100, 50, 5, 10);
            Enemy enemy = new Enemy("亚索", 100, 50, 5, 10);
            //是否刷新
            bool isFlush = true;
            Console.WriteLine("游戏开始!");
            player2.Print();
            enemy.Print();
            while (isStart)
            {
                Console.WriteLine("请输入a,q,r攻击,按w退出");
                char keyChar = Console.ReadKey().KeyChar;
                if (keyChar == 'w')
                {
                    isStart = false;
                }
                else
                {
                    player2.Handle(keyChar, enemy);
                    if (enemy.Hp < 0)
                    {
                        Console.WriteLine("你获得了胜利！");
                        break;
                    }
                    enemy.randomHandle(player2);
                    if (player2.Hp < 0)
                    {
                        Console.WriteLine("胜败乃兵家常事！");
                        break;
                    }

                }
                /*if (isFlush)
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

                }*/
            }



        }


    }
}
