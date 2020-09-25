
using GameCommon.Annotation;
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
        public static void start()
        {
            Type[] types = Assembly.GetExecutingAssembly().GetTypes();

            IEnumerable<Type> enumerable = types.Where(type => (type.GetCustomAttribute<Component>() != null));

            List<Type> lists = enumerable.ToList();
            foreach (var item in lists)
            {
                RegisterType(item);
            }
        }
        public static void RegisterType<T>(Object ob)
        {
            SingleInstanceDic.Add(typeof(T).Name, ob);

        }

        public static void RegisterType(Type type)
        {
           
            object v = Activator.CreateInstance(type, true);
            SingleInstanceDic.Add(type.Name , v);

        }

        public static T Resolve<T>()
        {
            object v = SingleInstanceDic[typeof(T).Name];
            return (T)v;
        }
    }


       
}
