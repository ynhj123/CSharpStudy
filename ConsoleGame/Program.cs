using ConsoleGame.Component;
using ConsoleGame.Controller;
using ConsoleGame.model;
using ConsoleGame.Service;
using GameCommon.Builder;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace ConsoleGame
{
    class Program
    {


        static void Main(string[] args)
        {
            InitNet();
            ContainerBuilder.Start(Assembly.GetExecutingAssembly().GetTypes());
            ScenceController scenceController = new ScenceController();
            scenceController.InitScence();
            scenceController.Handle();

        }
        public static void InitNet()
        {
            NetManagerEvent.AddListener("MsgEnter", OnEnter);
            NetManagerEvent.AddListener("MsgMove", OnMove);
            NetManagerEvent.AddListener("MsgLeave", OnLeave);
            NetManagerEvent.AddListener("MsgAttack", OnAttack);
            NetManagerEvent.AddListener("MsgRegistry", OnRegistry);
            NetManagerEvent.AddListener("MsgLogin", OnLogin);
            NetManagerEvent.AddListener("MsgListRoom", OnListRoom);
            NetManagerEvent.AddListener("MsgEnterRoom", OnEnterRoom);
            NetManagerEvent.AddListener("MsgGetRoomInfo", OnGetRoomInfo);
            NetManagerEvent.AddListener("MsgLeaveRoom", OnLeaveRoom);
            NetManagerEvent.AddListener("MsgPrepare", OnPrepare);
            NetManagerEvent.AddListener("MsgUnprepare", OnUnprepare);
            NetManagerEvent.AddListener("MsgStartBattle", OnStartBattle);
            NetManagerEvent.AddListener("MsgEndBattle", OnEndBattle);
            NetManagerEvent.Connect("192.168.1.178", 8888);

        }

        private static void OnEndBattle(MsgBase msgBase)
        {
            MsgEndBattle msg = (MsgEndBattle)msgBase;
            RoomDetailScence roomDetailScence = ContainerBuilder.Resolve<RoomDetailScence>();
            roomDetailScence.Room = JsonConvert.DeserializeObject<Room>(msg.result);
        }

        private static void OnGetRoomInfo(MsgBase msgBase)
        {
            MsgGetRoomInfo msg = (MsgGetRoomInfo)msgBase;
            RoomDetailScence roomDetailScence = ContainerBuilder.Resolve<RoomDetailScence>();
            roomDetailScence.Room = JsonConvert.DeserializeObject<Room>(msg.result);
        }

        private static void OnStartBattle(MsgBase msgBase)
        {
            MsgStartBattle msg = (MsgStartBattle)msgBase;
            if (msg.code == HttpStatusCode.OK)
            {
                GameSence gameSence = ContainerBuilder.Resolve<GameSence>();

                List<MsgStartBattle.StartPlay> startPlays = msg.startPlays;
                foreach (var item in startPlays)
                {
                    Player player = new Player(item.Id, 100, 50, item.X, item.Y, 'x', SwichColor(item.Index));
                    gameSence.AddSprite(player);
                }
                gameSence.Load();
                ScenceController.curScence = gameSence;
            }
        }
        private static ConsoleColor SwichColor(int index)
        {
            switch (index)
            {
                case 0:
                    return ConsoleColor.Red;

                case 1:
                    return ConsoleColor.DarkMagenta;

                case 2:
                    return ConsoleColor.Yellow;

                case 3:
                    return ConsoleColor.Green;

                case 4:
                    return ConsoleColor.Gray;

                case 5:
                    return ConsoleColor.Blue;

                case 6:
                    return ConsoleColor.Cyan;

                case 7:
                    return ConsoleColor.Magenta;

                case 8:
                    return ConsoleColor.White;
                default:
                    return ConsoleColor.White;
            }
        }

        private static void OnUnprepare(MsgBase msgBase)
        {
            MsgUnprepare msg = (MsgUnprepare)msgBase;
            RoomDetailScence roomDetailScence = ContainerBuilder.Resolve<RoomDetailScence>();
            roomDetailScence.Room.UserStatus[msg.result] = false;
        }

        private static void OnPrepare(MsgBase msgBase)
        {
            MsgPrepare msg = (MsgPrepare)msgBase;
            RoomDetailScence roomDetailScence = ContainerBuilder.Resolve<RoomDetailScence>();
            roomDetailScence.Room.UserStatus[msg.result] = true;
        }

        private static void OnLeaveRoom(MsgBase msgBase)
        {
            MsgLeaveRoom msg = (MsgLeaveRoom)msgBase;
            if (msg.code == HttpStatusCode.OK)
            {
                RoomDetailScence roomDetailScence = ContainerBuilder.Resolve<RoomDetailScence>();
                roomDetailScence.Room = JsonConvert.DeserializeObject<Room>(msg.result);

            }
            else
            {
                Console.WriteLine(msg.result);
            }
        }

        private static void OnEnterRoom(MsgBase msgBase)
        {
            MsgEnterRoom msg = (MsgEnterRoom)msgBase;
            if (msg.code == HttpStatusCode.OK)
            {
                RoomDetailScence roomDetailScence = ContainerBuilder.Resolve<RoomDetailScence>();
                roomDetailScence.Room = JsonConvert.DeserializeObject<Room>(msg.result);
                RoomScence roomScence = ContainerBuilder.Resolve<RoomScence>();
                roomScence.IsEnterRoomCallBack = true;
                ScenceController.curScence = ScenceController.scenceDict["roomDetail"];
            }
            else
            {
                Console.WriteLine(msg.result);
            }
        }

        private static void OnListRoom(MsgBase msgBase)
        {
            MsgListRoom msg = (MsgListRoom)msgBase;
            RoomScence roomScence = ContainerBuilder.Resolve<RoomScence>();
            if (msg.code == HttpStatusCode.OK)
            {
                Dictionary<int, Room> dictionaries = JsonConvert.DeserializeObject<Dictionary<int, Room>>(msg.result);
                roomScence.Rooms = dictionaries;
            }
            else
            {
                Console.WriteLine(msg.result);
            }


        }

        private static void OnLogin(MsgBase msgBase)
        {
            MsgLogin msgLogin = (MsgLogin)msgBase;
            LoginScence loginScence = ContainerBuilder.Resolve<LoginScence>();
            if (msgLogin.code != HttpStatusCode.OK)
            {
                Console.WriteLine(msgLogin.result);
            }
            else
            {
                User user = JsonConvert.DeserializeObject<User>(msgLogin.result);
                ScenceController.user = user;
                ScenceController.curScence = ScenceController.scenceDict["index"];
            }
            loginScence.IsLoignCallBack = true;
        }

        private static void OnRegistry(MsgBase msgBase)
        {
            MsgRegistry msgRegistry = (MsgRegistry)msgBase;
            LoginScence loginScence = ContainerBuilder.Resolve<LoginScence>();
            Console.WriteLine(msgRegistry.result);
            loginScence.IsResgistoryCallBack = true;

        }

        private static void OnLeave(MsgBase msgBase)
        {
            MsgLeave msgLeave = (MsgLeave)msgBase;
            GameSence gameSence = ContainerBuilder.Resolve<GameSence>();
            Sprite spr = gameSence.sprites.Where(sprite => sprite.Id == msgLeave.playId).FirstOrDefault();
            if (spr != null)
            {
                SpriteDestorySystem spriteDestorySystem = SpriteDestorySystem.GetSpriteDestorySystem();
                spriteDestorySystem.sprites.Enqueue(spr);
            }

        }
        private static void OnAttack(MsgBase msgBase)
        {
            MsgAttack msgAttack = (MsgAttack)msgBase;
            GameSence gameSence = ContainerBuilder.Resolve<GameSence>();
            List<Sprite> sprites = gameSence.sprites;
            Sprite sprite = sprites.Where(spirte => spirte.Id == msgAttack.playId).FirstOrDefault();
            if (sprite != null)
            {
                Player player = sprite as Player;
                player.attach(gameSence);
            }
        }

        private static void OnMove(MsgBase msgBase)
        {
            GameSence gameSence = ContainerBuilder.Resolve<GameSence>();
            MsgMove msgMove = (MsgMove)msgBase;
            System.Collections.Generic.List<Sprite> sprites = gameSence.sprites;
            foreach (var item in sprites)
            {
                if (item.Id == msgMove.spriteId)
                {
                    Debug.WriteLine("client move id : {0} , x :{1},y:{2}", msgMove.spriteId, msgMove.x, msgMove.y);
                    item.Position.X = msgMove.x;
                    item.Position.Y = msgMove.y;
                    item.Velocity.Veloctity = Enum.Parse<Veloctity>(msgMove.veloctity.ToString());

                }
            }
        }

        private static void OnEnter(MsgBase msgBase)
        {
            MsgEnter msgEnter = (MsgEnter)msgBase;
            List<MsgEnter> players = msgEnter.players;
            GameSence gameSence = ContainerBuilder.Resolve<GameSence>();


            //如果是第一次加入 开始游戏并获取列表
            //如果是别人加入，则新增一个player
            /*  if (gameSence.isStrat)
              {
                  Player player = new Player(100, msgEnter.x, msgEnter.y, Convert.ToChar(msgEnter.style));
                  player.Id = msgEnter.playId;
                  gameSence.AddSprite(player);
              }
              else
              {
                  foreach (MsgEnter enter in players)
                  {
                      if (enter.playId != msgEnter.playId)
                      {
                          Player player = new Player(100, enter.x, enter.y, Convert.ToChar(enter.style));
                          player.Id = enter.playId;
                          gameSence.AddSprite(player);
                      }
                  }
                  gameSence.isStrat = true;
                  gameSence.Handle();
              }*/

        }

        public async static Task InitJob()
        {
            JobController.Init();
            await JobController.Instance().startJob<PingJob>("myjob", "group", "trigger", "*/5 * * * * ?");

        }
    }
}
