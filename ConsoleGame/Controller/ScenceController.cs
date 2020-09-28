using ConsoleGame.model;
using GameCommon.Builder;
using System;
using System.Collections.Generic;

namespace ConsoleGame.Controller
{
    /**
     * 用于场景的跳转执行
     */
    public class ScenceController
    {
        public static Random random = new Random();
        public static Dictionary<string, Scence> scenceDict = new Dictionary<string, Scence>();
        public static User user;
        public static Scence curScence;
        public static bool isLeavel = false;


        public void InitScence()
        {

            scenceDict.Add("login", ContainerBuilder.Resolve<LoginScence>());
            scenceDict.Add("index", ContainerBuilder.Resolve<MenuScence>());
            scenceDict.Add("battle", InitGameScence());
            scenceDict.Add("room", ContainerBuilder.Resolve<RoomScence>());
            scenceDict.Add("user", ContainerBuilder.Resolve<UserScence>());
        }

        private Scence InitGameScence()
        {
            int x = random.Next(2, 20);
            int y = random.Next(2, 70);
            Player player = new Player(100, x, y, 'x');


            GameSence gameSence = GameSence.CreateGameSence(25, 80, 15);
            ListenDieSystem listenDieSystem = new ListenDieSystem(player, gameSence);
            KeywordSystem keywordSystem = new KeywordSystem(player, gameSence);
            CollisionSystem collisionSystem = new CollisionSystem(gameSence);
            AutoAttachIntervalSystem autoAttachSystem = new AutoAttachIntervalSystem(gameSence);
            SpriteDestorySystem spriteDestorySystem = SpriteDestorySystem.GetSpriteDestorySystem();

            gameSence.AddSystem(listenDieSystem);
            gameSence.AddSystem(keywordSystem);
            gameSence.AddSystem(collisionSystem);
            gameSence.AddSystem(autoAttachSystem);
            gameSence.AddSystem(spriteDestorySystem);
            gameSence.AddSprite(player);



            /*MsgEnter msgEnter = new MsgEnter();
            msgEnter.playId = player.Id;
            msgEnter.x = player.Position.X;
            msgEnter.y = player.Position.Y;
            msgEnter.veloctity = (int)player.Velocity.Veloctity;

            msgEnter.style = player.Style.ToString();

            NetManagerEvent.Send(msgEnter);
            while (!gameSence.isStrat)
            {
                NetManagerEvent.Update();
            }*/
            return gameSence;
        }

        public void Handle()
        {
            curScence = scenceDict["index"];
            while (!isLeavel)
            {
                curScence.Handle();
            }

        }
    }
}
