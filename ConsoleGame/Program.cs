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
            Player player = new Player(100, 2, 2, 'x');
            Player player1 = new Player(10, 5, 5, 'x');
            GameSence gameSence = new GameSence(25, 80, 2);
            KeywordSystem keywordSystem = new KeywordSystem(player, gameSence);
            gameSence.AddSystem(keywordSystem);
            gameSence.AddSprite(player);
            gameSence.AddSprite(player1);
            gameSence.Handle();
            /*initNet();
            InitJob();*/
            Console.ReadKey();
        }
        public static void initNet()
        {
            NetManagerEvent.Connect("127.0.0.1", 8888);
        }
        public async static Task InitJob()
        {
            JobController.Init();
            await JobController.Instance().startJob<PingJob>("myjob", "group", "trigger", "*/5 * * * * ?");

        }
    }
}
