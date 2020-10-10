using NetWorkUtils.Client;
using NetWorkUtils.Server;
using System;

namespace NetWorkUtils
{
    class Program
    {
        static void Main(string[] args)
        {
            NetManager.StartLoop(8888);
            
        }
    }
}
