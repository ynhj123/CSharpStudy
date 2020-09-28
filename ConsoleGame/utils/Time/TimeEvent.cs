using System;
using System.Threading;

namespace ConsoleGame.utils.Time
{
    class TimeEvent
    {
        /// <summary>
        /// 时间控制器
        /// </summary>
        /// <param name="timeIntvral"></param> 每隔几秒执行一次
        /// <param name="maxTime"></param> 最多执行几秒
        /// <param name="isFinsh"></param>  循环执行开关 true 不再循环
        /// <param name="timeHandle"></param> 循环执行方法
        /// <param name="timeOutHandle"></param> 循环超时方法
        public static void Handle(int timeIntvral, int maxTime, ref bool isFinsh, TimeHandle timeHandle, TimeOutHandle timeOutHandle)
        {
            int initTime = 0;
            while ((maxTime > initTime) && (!isFinsh))        //  '循环等待 
            {
                timeHandle();                            // '转让控制权，以便让操作系统处理其它的事件。 
                Thread.Sleep(1000 * timeIntvral);
                initTime += timeIntvral;

            }
            if (!isFinsh)
            {
                timeOutHandle();

            }

        }
        public static long GetTimeStamp()
        {
            TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);
        }

        public delegate void TimeHandle();
        public delegate void TimeOutHandle();

    }
}
