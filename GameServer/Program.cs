using GameServer.script.db;
using GameServer.script.net;
using System;

namespace GameServer
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!DBManager.Connect("game", "127.0.0.1", 3306, "root", "root123"))
            {
                return;
            }
            NetManager.StartLoop(8888);
        }
    }
}
