using GameCommon.Annotation;
using GameCommon.Builder;
using System;
using System.Linq;
using System.Reflection;

namespace GameCommon
{
    class Program
    {
        public static Assembly asm;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string path = @baseDirectory + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Namespace + ".dll";
            
            asm = Assembly.LoadFile(path);
            Type[] types = asm.GetTypes();
            /*types.Where(type => type.GetCustomAttributes().Where(attribute => attribute.)) ;*/
            
            foreach (var item in types)
            {
                Attribute attribute = item.GetCustomAttribute(typeof(Component));
                Component component = item.GetCustomAttribute<Component>();
                System.Collections.Generic.IEnumerable<Attribute> enumerable = item.GetCustomAttributes();
                foreach (var items in enumerable)
                {
                    Type type = items.GetType();
                    if (type.Name == typeof(Component).Name)
                    {
                        ContainerBuilder.RegisterType(item);

                    }
                   
                }
               
                
                

            }
            TestController testController = ContainerBuilder.Resolve<TestController>();
            testController.hello();
           
            Console.ReadKey();
        }
    }
}
