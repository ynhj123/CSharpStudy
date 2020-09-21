using Quartz;
using System.Threading.Tasks;

namespace ConsoleGame.Service
{
    class PingJob : IJob
    {

        public virtual Task Execute(IJobExecutionContext context)
        {
            NetManagerEvent.Update();
            return null;
        }


    }
}
