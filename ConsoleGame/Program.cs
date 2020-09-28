using ConsoleGame.Component;
using ConsoleGame.Controller;
using ConsoleGame.model;
using ConsoleGame.Service;
using GameCommon.Builder;
using Newtonsoft.Json;
using System;
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
            NetManagerEvent.Connect("192.168.1.178", 8888);

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
            throw new NotImplementedException();
        }
        private static void OnAttack(MsgBase msgBase)
        {
            MsgAttack msgAttack = (MsgAttack)msgBase;
            GameSence gameSence = GameSence.getGameScence();
            System.Collections.Generic.List<Sprite> sprites = gameSence.sprites;
            Sprite sprite = sprites.Where(spirte => spirte.Id == msgAttack.playId).First();
            Player player = sprite as Player;
            player.attach(gameSence);
        }

        private static void OnMove(MsgBase msgBase)
        {
            GameSence gameSence = GameSence.getGameScence();
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
            System.Collections.Generic.List<MsgEnter> players = msgEnter.players;
            GameSence gameSence = GameSence.getGameScence();


            //如果是第一次加入 开始游戏并获取列表
            //如果是别人加入，则新增一个player
            if (gameSence.isStrat)
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
            }

        }

        public async static Task InitJob()
        {
            JobController.Init();
            await JobController.Instance().startJob<PingJob>("myjob", "group", "trigger", "*/5 * * * * ?");

        }
    }
}
