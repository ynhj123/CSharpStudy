using Quartz;
using Quartz.Impl;
using System.Threading.Tasks;

namespace ConsoleGame.Controller
{
    public class JobController
    {
        private static JobController jobController;
        static IScheduler scheduler;

        private JobController()
        {

        }
        public static JobController Instance()
        {

            if (jobController == null)
            {
                jobController = new JobController();
            }

            return jobController;
        }
        public async static void Init()
        {
            // 1.创建scheduler的引用
            ISchedulerFactory schedFact = new StdSchedulerFactory();
            scheduler = await schedFact.GetScheduler();

            //2.启动 scheduler
            await scheduler.Start();

        }

        public async Task startJob<T>(string jobName, string groupName, string triggerName, string corn) where T : IJob
        {

            IJobDetail job = JobBuilder.Create<T>()
                .WithIdentity(jobName, groupName)
                .Build();
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(triggerName, groupName)
                //"0 42 10 * * ?"
                .WithCronSchedule(corn)
                .ForJob(jobName, groupName)
                .Build();
            await scheduler.ScheduleJob(job, trigger);
        }

    }
}
