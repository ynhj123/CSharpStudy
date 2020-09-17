using Quartz;
using System;
using System.Threading.Tasks;

namespace ConsoleGame.Service
{
    class PingJob : IJob
    {

        public virtual Task Execute(IJobExecutionContext context)
        {
            NetManagerEvent.PingUpdate();
            return Console.Out.WriteLineAsync("send ping");
        }


    }
}
