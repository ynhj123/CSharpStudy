using GameCommon.Annotation;
using GameCommon.Builder;
using System;
using System.Linq;
using System.Reflection;

namespace GameCommon
{
    class Program
    {
     
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ContainerBuilder.start();
            TestController testController = ContainerBuilder.Resolve<TestController>();
            testController.hello();
           
            Console.ReadKey();
        }
    }
}
