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
        protected static int origRow;
        protected static int origCol;

        public List<Sprite> sprites = new List<Sprite>();
        public List<IExecuteSystem> systems = new List<IExecuteSystem>();

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        private GameSence()
        {

        }
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
            InitMap(x, y);
            return sence;

        }
        public static GameSence getGameScence()
        {
            return sence;
        }

        private static void InitMap(int x, int y)
        {
            
            Console.Clear();
            Console.CursorVisible = false;

            origRow = Console.CursorTop;
            origCol = Console.CursorLeft;
            char[,] map = sence.map;
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    //边框
                    if (i == 0 || i == x - 1 || j == 0 || j == y - 1)
                    {
                        //map[i, j] = '#';
                        WriteAt("#", i, j);
                    }

                    /*else
                    {
                        map[i, j] = ' ';
                        
                    }*/
                }
            }
        }
        private static void ReflushMap()
        {

            List<Sprite> sprites = sence.sprites;
            for (int i = 0; i < sprites.Count; i++)
            {

                //map[sprites[i].Position.X, sprites[i].Position.Y] = sprites[i].Style;
                WriteAt(" ", sprites[i].Position.X, sprites[i].Position.Y);
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
                ReflushMap();
                NetManagerEvent.Update();
                foreach (IExecuteSystem system in systems)
                {
                    system.Execute();
                }
                /*initMap(this.X, this.Y);*/
                for (int i = 0; i < sprites.Count; i++)
                {
                    bool isMove = sprites[i].Move(this);
                    //map[sprites[i].Position.X, sprites[i].Position.Y] = sprites[i].Style;
                    WriteAt(sprites[i].Style.ToString(), sprites[i].Position.X, sprites[i].Position.Y);
                }


                //Print();
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
        protected static void WriteAt(string s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(origCol + y, origRow + x);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
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
