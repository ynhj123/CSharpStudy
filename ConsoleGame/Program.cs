using ConsoleGame.Controller;
using ConsoleGame.Service;
using System;
using System.Threading.Tasks;

namespace ConsoleGame
{
    class Program
    {
        static void Main(string[] args)
        {
            initNet();
            InitJob();
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
