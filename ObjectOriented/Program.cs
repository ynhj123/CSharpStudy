using ObjectOriented.OverrideAndInterface;
using System;

namespace ObjectOriented
{
    /**
     * 
     */
    class Program
    {
        //是否进行
        static bool isStart = true;
        static void Main(string[] args)
        {
            Goods java = new Goods();
            java.Name = "java";
            java.Num = 10;
            java.Price = 9.5;
            Goods csharp = new Goods();
            csharp.Name = "csharp";
            csharp.Num = 4;
            csharp.Price = 5.5;
            Goods python = new Goods();
            python.Name = "python";
            python.Num = 7;
            python.Price = 7.5;
            DiscountOrder discountOrder = new DiscountOrder(0.5);
            discountOrder.add(csharp);
            discountOrder.add(java);
            FullReductionOrder fullReductionOrder = new FullReductionOrder();
            fullReductionOrder.add(python);

            AccountBook accountBook = new AccountBook();
            accountBook.add(discountOrder);
            accountBook.add(fullReductionOrder);

            Console.WriteLine("合计 {0}", accountBook.totalPrice());
            /*Scence scence = new Scence(20, 20);
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

                }*/
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
            /*}*/



        }


    }
}
