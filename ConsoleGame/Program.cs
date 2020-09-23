using ConsoleGame.Controller;
using ConsoleGame.model;
using ConsoleGame.Service;
using System;
using System.Threading.Tasks;

namespace ConsoleGame
{
    class Program
    {
        static void Main(string[] args)
        {
            InitNet();
            InitJob();
            Player player = new Player(100, 2, 2, 'x');
            Player player1 = new Player(10, 5, 5, 'x');

            GameSence gameSence = new GameSence(25, 80, 2);
            ListenDieSystem listenDieSystem = new ListenDieSystem(player, gameSence);
            KeywordSystem keywordSystem = new KeywordSystem(player, gameSence);
            CollisionSystem collisionSystem = new CollisionSystem(gameSence);
            AutoAttachSystem autoAttachSystem = new AutoAttachSystem(gameSence);
            autoAttachSystem.AddAutoPlayer(player1);
            gameSence.AddSystem(listenDieSystem);
            gameSence.AddSystem(keywordSystem);
            gameSence.AddSystem(collisionSystem);
            gameSence.AddSystem(autoAttachSystem);
            gameSence.AddSprite(player);
            gameSence.AddSprite(player1);
            gameSence.Handle();

            Console.ReadKey();
        }
        public static void InitNet()

        {
            NetManagerEvent.AddListener("Enter", OnEnter);
            NetManagerEvent.AddListener("Move", OnMove);
            NetManagerEvent.AddListener("Leave", OnLeave);
            NetManagerEvent.Connect("127.0.0.1", 8888);

        }

        private static void OnLeave(MsgBase msgBase)
        {
            throw new NotImplementedException();
        }

        private static void OnMove(MsgBase msgBase)
        {
            throw new NotImplementedException();
        }

        private static void OnEnter(MsgBase msgBase)
        {
            throw new NotImplementedException();
        }

        public async static Task InitJob()
        {
            JobController.Init();
            await JobController.Instance().startJob<PingJob>("myjob", "group", "trigger", "*/5 * * * * ?");

        }
    }
}
