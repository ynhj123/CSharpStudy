using System;

namespace test3
{
    class Program
    {
        static void Main(string[] args)

        {
            (int key, string val) direct = (1, "ok");
            Console.WriteLine($@"good study {direct.key} , day day up {direct.val}");
            Console.WriteLine(
@"**********************************
*   猜数字游戏开始(1-100之间)    *
**********************************");
            GuessNum();
            Console.WriteLine(GetLimitRandomStr("godstudydayup", 10));
            /*Console.WriteLine("通用绘图函数");

            drawGraphicsFunc(25, 100, 0.33, Math.PI * 3/100 , Math.PI * 4, Math.PI * 4, (x) =>
            {
                return Math.Sin(x);
            });*/
            /* Console.WriteLine(" 打印sin函数");
             PrintSinFunc();
             Console.WriteLine(" 打印等腰三角形2");
             PrintIsoscelesTriangle2();
             Console.WriteLine(" 打印直角三角形2");
             PrintRightTriangle2();
             Console.WriteLine(" 打印等腰三角形1");
             PrintIsoscelesTriangle1();
             Console.WriteLine(" 打印直角三角形1");
             PrintRightTriangle1();
             Console.WriteLine(" 打印99乘法表");
             PrintMultipleTable();
             Console.WriteLine(" 打印3*4表格");
             PrintTable();
             //打印乘2的数
             PrintTwoMultiple();
             int v = CardType.zhangfei.GetHashCode();
             CardType v1 = (CardType)Enum.Parse(typeof(CardType), "1");
             Console.WriteLine(v1);
             //循环练习
             Console.WriteLine("请输入数字c：");
             string cStr = Console.ReadLine();
             int c = Convert.ToInt32(cStr);
             Console.WriteLine("输出1-c的每一个数及和");
             PrintEvevyNumAndSum(c);
             Console.WriteLine("输出1-c的3和5的倍数及和");
             PrintThreeOrFiveMultipleNumAndSum(c);
             Console.WriteLine("输出1-c的和及乘积");
             PrintSumAndeMultiple(c);

             //输入输出练习
             Console.WriteLine("Hello World!");
             Console.WriteLine("请输入用户姓名");
             string username = Console.ReadLine();
             Console.WriteLine("Hello " + username);

             //数字交换
             Console.WriteLine("数字交换开始");
             Console.WriteLine("请输入数字a：");
             string aStr = Console.ReadLine();
             int a = Convert.ToInt32(aStr);
             Console.WriteLine("请输入数字b：");
             string bStr = Console.ReadLine();
             int b = Convert.ToInt32(bStr);
             Compare(ref a, ref b);
             Console.WriteLine("数字交换后");
             Console.WriteLine("a=" + a);
             Console.WriteLine("b=" + b);

             //批量随机抽卡 
             Console.WriteLine("请输入要抽的次数：");
             string sizeStr = Console.ReadLine();
             int size = Convert.ToInt32(sizeStr);
             if (size <= 0)
             {
                 Console.WriteLine("请输入大于0的次数");
             }
             for (int i = 0; i < size; i++)
             {
                 CardType card = RandomCard();
                 Console.WriteLine("恭喜你抽到了：" + card);
             }*/
            Console.ReadKey();

        }
        /**
         * 猜数字游戏，范围1-100之间
         */
        static void GuessNum()
        {
            int guessSize = 0;
            int orginNum = InitRandomNum();
            while (true)
            {
                int guessNum = GetInputNum();
                if (guessNum <= 0 || guessNum > 100)
                {
                    Console.WriteLine("转换异常！");
                }
                else if (guessNum == orginNum)
                {
                    Console.WriteLine("恭喜你，猜对了，共猜了{0}次！", guessSize);
                    return;
                }
                else if (guessNum < orginNum)
                {
                    Console.WriteLine("你猜小了");
                    guessSize++;
                }
                else
                {
                    Console.WriteLine("你猜大了");
                    guessSize++;
                }

            }
        }

        /**
         *  获取输入值
         */
        static int GetInputNum()
        {
            int inputNum;
            Console.WriteLine("请输入一个数字");
            string guessNumStr = Console.ReadLine();
            bool isSuccess = int.TryParse(guessNumStr, out inputNum);
            if (isSuccess)
            {
                return inputNum;
            }
            else
            {
                return -1;
            }
        }

        /**
         * 初始化一个随机数
         */
        static int InitRandomNum()
        {
            if (random == null)
            {
                random = new Random();
            }
            return random.Next(1, 101);
        }

        /**
         *  随机将打印size个randomStr字符串的内容
         */
        static String GetLimitRandomStr(String initStr, int size)
        {
            if (random == null)
            {
                random = new Random();
            }
            String randomStr = "";
            for (int i = 0; i < size; i++)
            {
                int randomInt = random.Next(0, initStr.Length);
                randomStr += initStr[randomInt];
            }
            return randomStr;

        }
        /**
         *  a+b
         */
        static int Add(int a, int b)
        {
            return a + b;
        }
        static void drawGraphicsFunc(int x, int y, double deviation, double m, double n, double z, handleTriangleFunc func)
        {
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    double val = func(j * m) * n + z;
                    if (Math.Abs(val - i) < deviation)
                    {
                        Console.Write("*");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
        }



        /**
         *  打印sin函数 图形范围成像
         */
        static void PrintSinFunc()
        {
            int col = -2, row = 10;
            for (int i = 1; i > col; i--)
            {
                for (int j = 0; j < row; j += 1)
                {
                    if (Math.Round(Math.Sin(j * Math.PI / 2)) == i)
                    {
                        Console.Write("*");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
        }


        /**
         * 打印等腰三角形2
         * 7*7 矩阵 不显示部分值 简单循环代替复杂循环
         */
        static void PrintIsoscelesTriangle2()
        {
            int col = 7, row = 7;
            for (int i = 0; i < col; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    //前段打印直角2
                    if (j < 3)
                    {
                        if (j > 5 - i)
                        {
                            Console.Write("*");

                        }
                        else
                        {
                            Console.Write(" ");
                        }
                    }
                    //后段打印直角1
                    if (j >= 3)
                    {
                        if (j > i)
                        {
                            continue;
                        }
                        Console.Write("*");
                    }
                }
                Console.WriteLine();
            }
        }
        /**
         * 打印直角三角形2
         * 5*5 矩阵 打印部分空值
         */
        static void PrintRightTriangle2()
        {
            int col = 5, row = 5;
            for (int i = 0; i < col; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    if (j > 4 - i)
                    {
                        Console.Write("*");

                    }
                    else
                    {
                        Console.Write(" ");
                    }

                }
                Console.WriteLine();
            }
        }
        /**
         * 打印等腰三角形1
         */
        static void PrintIsoscelesTriangle1()
        {
            int col = 5, row = 5;
            for (int i = 0; i < col; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    //分2段打印 
                    //前段打印直角1
                    if (j > i && i < 3)
                    {
                        continue;
                    }
                    //后段打印反直角1
                    if (j < i && i >= 3)
                    {
                        continue;
                    }
                    Console.Write("*");
                }
                Console.WriteLine();
            }
        }
        /**
         * 打印直角三角形
         * 5*5 矩阵 不显示部分值
         */
        static void PrintRightTriangle1()
        {
            int col = 5, row = 5;
            for (int i = 0; i < col; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    if (j > i)
                    {
                        continue;
                    }
                    Console.Write("*");
                }
                Console.WriteLine();
            }
        }
        static void PrintMultipleTable()
        {
            int col = 9, row = 9;
            for (int i = 0; i < col; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    if (j > i)
                    {
                        continue;
                    }
                    Console.Write("{0}*{1}={2} ", j + 1, i + 1, (i + 1) * (j + 1));
                }
                Console.WriteLine();
            }
        }
        /**
         * 打印3*4表格
         */
        static void PrintTable()
        {
            int col = 3, row = 4;
            for (int i = 0; i < col; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    Console.Write("{0}_{1} ", i + 1, j + 1);
                }
                Console.WriteLine();
            }
        }
        /**
         *  循环将输入的数乘以2输出，如果不能转换打印不能转换，输如end跳出循环
         */
        static void PrintTwoMultiple()
        {
            while (true)
            {
                Console.WriteLine("请输入一个需要求2倍的数num（输入END结束）");
                string numStr = Console.ReadLine();
                numStr = numStr.ToUpper();
                if (numStr == "END")
                {
                    return;
                }
                int num;
                bool isSuccess = int.TryParse(numStr, out num);
                if (isSuccess)
                {
                    num *= 2;
                    Console.WriteLine("num * 2 = {0}", num);
                }
                else
                {
                    Console.WriteLine("转换失败!");
                }
            }
        }
        /**
         * 打印求和和乘积值
         */
        static void PrintSumAndeMultiple(int n)
        {
            int tmp = n;
            int sum = 0;
            while (n >= 1)
            {

                sum += n;
                n--;
            }
            Console.WriteLine("和=" + sum);
            int multiple = 1;
            n = 1;
            while (n <= tmp)
            {
                multiple *= n;
                n++;
            }
            Console.WriteLine("乘积=" + multiple);

        }
        /**
         * 打印1-n每一个数并求和
         */
        static void PrintEvevyNumAndSum(int n)
        {
            int sum = 0;
            while (n >= 1)
            {
                Console.WriteLine(n);
                sum += n;
                n--;
            }
            Console.WriteLine("和=" + sum);

        }
        /**
         * 打印1-n 之间3，5的倍数并求和
         */
        static void PrintThreeOrFiveMultipleNumAndSum(int n)
        {
            int sum = 0;
            while (n >= 1)
            {
                if (IsThreeOrFiveMultipleNum(n))
                {
                    Console.WriteLine(n);
                    sum += n;
                }
                n--;
            }
            Console.WriteLine("和=" + sum);

        }
        /**
         * 判断一个数是否是三或者5的倍数
         */
        static bool IsThreeOrFiveMultipleNum(int n)
        {
            if (n % 3 == 0 || n % 5 == 0)
            {
                return true;
            }
            return false;
        }
        /**
         * 交换2个数
         */
        static void Compare(ref int a, ref int b)
        {
            int c = a;
            a = b;
            b = c;
        }
        /**
         * 枚举卡片类型
         */
        enum CardType
        {
            guanyu,
            zhangfei,
            zhaoyun,
            huangzhong,

        }
        static Random random; //伪随机数 根据当前时间获取 线性同余算法
        /**
         * 随机获取一个卡片类型
         */
        static CardType RandomCard()
        {
            if (random == null)
            {
                random = new Random();
            }
            int rInt = random.Next(0, 10);
            if (rInt < 1)
            {
                return CardType.guanyu;
            }
            else if (rInt < 3)
            {
                return CardType.zhangfei;
            }
            else if (rInt < 6)
            {
                return CardType.zhaoyun;
            }
            else
            {
                return CardType.huangzhong;
            }

        }
        delegate double handleTriangleFunc(double param);

    }




}
