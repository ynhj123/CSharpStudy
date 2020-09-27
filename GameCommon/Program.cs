using GameCommon.Builder;
using System;

namespace GameCommon
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ContainerBuilder.Start();
            TestController testController = ContainerBuilder.Resolve<TestController>();
            testController.Hello();


            Console.ReadKey();
        }
    }
}
