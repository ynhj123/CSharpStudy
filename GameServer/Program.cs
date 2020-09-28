using GameServer.script.db;
using GameServer.script.net;

namespace GameServer
{
    class Program
    {
        static void Main(string[] args)
        {
            UserManager.Load();
            NetManager.StartLoop(8888);
        }
    }
}
