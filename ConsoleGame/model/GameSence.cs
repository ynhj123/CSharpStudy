using ConsoleGame.Controller;
using System;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleGame.model
{
    public class GameSence
    {
        int x, y;
        int interval;
        char[,] map;
        public bool isStrat = true;
        public bool isWin = false;

        public List<Sprite> sprites = new List<Sprite>();
        public List<IExecuteSystem> systems = new List<IExecuteSystem>();

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }

        public GameSence(int x, int y, int interval)
        {
            this.X = x;
            this.Y = y;

            this.interval = interval;
            this.map = new char[x, y];
            initMap(x, y);

        }

        private void initMap(int x, int y)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    //边框
                    if (i == 0 || i == x - 1 || j == 0 || j == y - 1)
                    {
                        map[i, j] = '#';
                    }

                    else
                    {
                        map[i, j] = ' ';
                    }
                }
            }
        }

        public void AddSprite(Sprite sprite)
        {
            sprites.Add(sprite);
        }
        public void RemoveSprite(Sprite sprite)
        {
            sprites.Remove(sprite);
        }
        public void AddSystem(IExecuteSystem system)
        {
            systems.Add(system);
        }
        public void RemoveSystem(IExecuteSystem system)
        {
            systems.Remove(system);
        }


        public void Handle()
        {

            while (isStrat)
            {
                foreach (IExecuteSystem system in systems)
                {
                    system.Execute();
                }
                initMap(this.X, this.Y);
                for (int i = 0; i < sprites.Count; i++)
                {
                    bool isMove = sprites[i].Move(this);
                    if (isMove)
                    {
                        map[sprites[i].Position.X, sprites[i].Position.Y] = sprites[i].Style;

                    }
                }


                Print();
                Thread.Sleep(1000 / this.interval);
            }
            if (isWin)
            {
                Console.WriteLine("game win");

            }
            else
            {
                Console.WriteLine("game over");

            }


        }

        private void Print()
        {
            Console.Clear();
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i, j]);
                }
                Console.WriteLine();
            }
        }
    }

}
