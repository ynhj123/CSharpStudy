
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GameCommon.Builder
{
    public class ContainerBuilder
    {

        private static Dictionary<string, Object> SingleInstanceDic = new Dictionary<string, object>();

        private ContainerBuilder()
        {
        
        }
        public static void RegisterType<T>(Object ob)
        {
            SingleInstanceDic.Add(typeof(T).Name, ob);

        }

        public static void RegisterType(Type type)
        {
            object v = Activator.CreateInstance(type, true);
            object v2 = Program.asm.CreateInstance(type.FullName);
            TestController v1 = (TestController)v2;
            v1.hello();
            SingleInstanceDic.Add(type.Name , v);

        }

        public static T Resolve<T>()
        {
            object v = SingleInstanceDic[typeof(T).Name];
            return (T)v;
        }
    }


       
}
