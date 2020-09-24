using ConsoleGame.Controller;
using System;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleGame.model
{
    public class GameSence
    {
        static GameSence sence;
        int x, y;
        int interval;
        char[,] map;
        public bool isStrat = false;
        public bool isWin = false;


        public List<Sprite> sprites = new List<Sprite>();
        public List<IExecuteSystem> systems = new List<IExecuteSystem>();

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }


        public static GameSence CreateGameSence(int x, int y, int interval)
        {
            if (sence == null)
            {
                sence = new GameSence();
            }
            sence.X = x;
            sence.Y = y;

            sence.interval = interval;
            sence.map = new char[x, y];
            initMap(x, y);
            return sence;

        }
        public static GameSence getGameScence()
        {
            return sence;
        }

        private static void initMap(int x, int y)
        {
            char[,] map = sence.map;
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
                NetManagerEvent.Update();
                foreach (IExecuteSystem system in systems)
                {
                    system.Execute();
                }
                initMap(this.X, this.Y);
                for (int i = 0; i < sprites.Count; i++)
                {
                    bool isMove = sprites[i].Move(this);
                    map[sprites[i].Position.X, sprites[i].Position.Y] = sprites[i].Style;
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
