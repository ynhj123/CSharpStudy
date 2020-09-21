using GameServer.script.net;

namespace GameServer
{
    class Program
    {
        static void Main(string[] args)
        {

            NetManager.StartLoop(8888);
        }
    }
}
