using System;
using System.Collections.Generic;
using System.Threading;

namespace SimpleMap
{
    class Player
    {
        public int x;
        public int y;

        public int score = 0;
    }

    class Point
    {
        public int x;
        public int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }


    class Program
    {
        //static Player player;

        // 游戏世界的大小
        static int map_width = 35;
        static int map_height = 20;

        static int offset_x = 1;
        static int offset_y = 2;

        static char[,] buffer = new char[map_height + offset_y + 2, map_width + offset_x + 2];

        static Player player;
        static List<Point> points;

        static Random random = new Random();

        static void ClearBuffer()
        {
            for (int i = 0; i < buffer.GetLength(0); i++)
            {
                for (int j = 0; j < buffer.GetLength(1); j++)
                {
                    buffer[i, j] = ' ';
                }
            }
        }

        static void Refresh()
        {
            ClearBuffer();
            DrawBorder();
            DrawPlayer();
            DrawPoints();
            // 显示分数
            string s = player.score.ToString();
            for (int i = 0; i < s.Length; i++)
            {
                buffer[0, i] = s[i];
            }

            Console.Clear();

            // 将buffer内容打印到屏幕
            for (int i = 0; i < buffer.GetLength(0); i++)
            {
                for (int j = 0; j < buffer.GetLength(1); j++)
                {
                    Console.Write(buffer[i, j]);
                }
                Console.WriteLine();
            }

        }

        static void DrawPlayer()
        {
            buffer[player.y + offset_y, player.x + offset_x] = 'x';
        }

        static void DrawPoints()
        {
            for (int i = 0; i < points.Count; i++)
            {
                var p = points[i];
                buffer[p.y + offset_y, p.x + offset_x] = '.';
            }
        }

        // 画边界
        static void DrawBorder()
        {
            string[] back = new string[] { "#####################################",
                                           "#                                   #",
                                           "#                                   #",
                                           "#                                   #",
                                           "#              OOOO                 #",
                                           "#              O  O                 #",
                                           "#              O  O                 #",
                                           "#                                   #",
                                           "#                                   #",
                                           "#                                   #",
                                           "#                                   #",
                                           "#                                   #",
                                           "#                                   #",
                                           "#                                   #",
                                           "#                                   #",
                                           "#                                   #",
                                           "#                                   #",
                                           "#                                   #",
                                           "#                                   #",
                                           "#                                   #",
                                           "#                                   #",
                                           "#####################################",};
            for (int y = 0; y < back.Length; y++)
            {
                for (int x = 0; x < back[0].Length; x++)
                {
                    buffer[y + offset_y - 1, x + offset_x - 1] = back[y][x];
                }
            }

            //// 上边
            //for (int i = offset_x; i < map_width+offset_x+1 ; ++i)
            //{
            //    buffer[offset_y-1, i] = '#';
            //}

            //// 左边
            //for (int i = 0; i < map_height+offset_y+1 ; ++i)
            //{
            //    buffer[i, 0] = '#';
            //}

            //// 下边
            //for (int i = 0; i < map_width+offset_x+2 ; ++i)
            //{
            //    buffer[map_height+offset_y , i] = '#';
            //}
            //// 右边
            //for (int i = 0; i < map_height+offset_y+1 ; ++i)
            //{
            //    buffer[i, map_width+1] = '#';
            //}
        }

        static void Main(string[] args)
        {
            points = new List<Point>();
            points.Add(new Point(0, 0));
            points.Add(new Point(14, 9));

            player = new Player();

            Refresh();

            while (true)
            {
                ConsoleKey key = ConsoleKey.Z;
                if (Console.KeyAvailable)
                {
                    key = Console.ReadKey(true).Key;
                }
                switch (key)
                {
                    case ConsoleKey.W:
                        player.y -= 1;
                        if (player.y < 0)
                        {
                            player.y = 0;
                        }
                        break;
                    case ConsoleKey.S:
                        player.y += 1;
                        if (player.y >= map_height)
                        {
                            player.y = map_height - 1;
                        }
                        break;
                    case ConsoleKey.A:
                        player.x -= 1;
                        if (player.x < 0)
                        {
                            player.x = 0;
                        }
                        break;
                    case ConsoleKey.D:
                        player.x += 1;
                        if (player.x >= map_width)
                        {
                            player.x = map_width - 1;
                        }
                        break;
                }

                // 碰撞检测
                for (int i = 0; i < points.Count; i++)
                {
                    var p = points[i];
                    if (player.x == p.x && player.y == p.y)
                    {
                        points.RemoveAt(i);
                        // 加一个
                        int x = random.Next(0, map_width);
                        int y = random.Next(0, map_height);
                        points.Add(new Point(x, y));

                        player.score += 1234;
                        break;
                    }
                }

                Refresh();
                Thread.Sleep(100);
            }


        }
    }
}
