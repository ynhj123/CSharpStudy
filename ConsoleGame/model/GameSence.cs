using ConsoleGame.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ConsoleGame.model
{
    [GameCommon.Ioc.Annotation.Component]
    public class GameSence : Scence
    {

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

        public void Load()
        {
            this.Load(25, 80, 15);
            this.isStrat = true;
            Player player = sprites.Where(sprite => sprite.GetType() == typeof(Player)).Select(sprites => (Player)sprites).Where(player => player.Id == ScenceController.user.Userid).FirstOrDefault();
            ListenDieSystem listenDieSystem = new ListenDieSystem(player, this);
            KeywordSystem keywordSystem = new KeywordSystem(player, this);
            CollisionSystem collisionSystem = new CollisionSystem(this);
            AutoAttachIntervalSystem autoAttachSystem = new AutoAttachIntervalSystem(this);
            SpriteDestorySystem spriteDestorySystem = SpriteDestorySystem.GetSpriteDestorySystem();

            this.AddSystem(listenDieSystem);
            this.AddSystem(keywordSystem);
            this.AddSystem(collisionSystem);
            this.AddSystem(autoAttachSystem);
            this.AddSystem(spriteDestorySystem);
        }
        private void Load(int x, int y, int interval)
        {

            this.X = x;
            this.Y = y;

            this.interval = interval;
            this.map = new char[x, y];
            

        }



        private void InitMap(int x, int y)
        {

            Console.Clear();
            Console.CursorVisible = false;

            origRow = Console.CursorTop;
            origCol = Console.CursorLeft;
            char[,] map = this.map;
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
        private void ReflushMap()
        {

            List<Sprite> sprites = this.sprites;
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
            InitMap(x, y);
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
                    if(sprites[i] is Player)
                    {
                        Player Player = (Player)sprites[i];
                        WriteAt(Player.Style.ToString(), sprites[i].Position.X, sprites[i].Position.Y, Player.Color);
                    }
                    else
                    {
                        WriteAt(sprites[i].Style.ToString(), sprites[i].Position.X, sprites[i].Position.Y);
                    }
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
            Thread.Sleep(1000);
            ScenceController.curScence = ScenceController.scenceDict["roomDetail"];


        }
        protected static void WriteAt(string s, int x, int y, ConsoleColor color = ConsoleColor.White)
        {
            try
            {
                Console.ForegroundColor = color;
                Console.SetCursorPosition(origCol + y, origRow + x);
                Console.Write(s);
                Console.ResetColor();
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
