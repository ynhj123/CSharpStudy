

using GameCommon.Ioc.Annotation;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;

namespace GameCommon.Builder
{
    public class ContainerBuilder
    {

        private static Dictionary<string, Object> SingleInstanceDic = new Dictionary<string, object>();
        private static Dictionary<string, List<FieldInfo>> AutoWiredFiledDic = new Dictionary<string, List<FieldInfo>>();

        private ContainerBuilder()
        {

        }

        public static void RegisterType<T>()
        {
            throw new NotImplementedException();
        }

        public static void Start()
        {
            Type[] types = Assembly.GetExecutingAssembly().GetTypes();

            IEnumerable<Type> enumerable = types.Where(type => (type.GetCustomAttribute<Component>() != null));

            List<Type> lists = enumerable.ToList();
            foreach (var item in lists)
            {
                RegisterType(item);
            }
            //AutoWired
            foreach (var item in AutoWiredFiledDic)
            {
                object v = SingleInstanceDic[item.Key];
                foreach (var filed in item.Value)
                {
                    object v1 = SingleInstanceDic[filed.FieldType.Name];
                    if (v1 == null)
                    {
                        throw new SystemException("AutoWired failed");
                    }
                    filed.SetValue(v, v1);
                }

            }
        }

        public static void Start(Type[] types)
        {


            IEnumerable<Type> enumerable = types.Where(type => (type.GetCustomAttribute<Component>() != null));

            List<Type> lists = enumerable.ToList();
            foreach (var item in lists)
            {
                RegisterType(item);
            }
            //AutoWired
            foreach (var item in AutoWiredFiledDic)
            {
                object v = SingleInstanceDic[item.Key];
                foreach (var filed in item.Value)
                {
                    object v1 = SingleInstanceDic[filed.FieldType.Name];
                    if (v1 == null)
                    {
                        throw new SystemException("AutoWired failed");
                    }
                    filed.SetValue(v, v1);
                }

            }
        }
        public static void RegisterType<T>(Object ob)
        {
            SingleInstanceDic.Add(typeof(T).Name, ob);

        }

        public static void RegisterType(Type type)
        {

            object o = Activator.CreateInstance(type, true);



            foreach (PropertyInfo p in type.GetProperties())
            {
                string v = ConfigurationManager.AppSettings[p.Name];
                if (!String.IsNullOrEmpty(v))
                {
                    p.SetValue(o, v);

                }
            }
            SingleInstanceDic.Add(type.Name, o);

            FieldInfo[] fieldInfos = type.GetFields();
            List<FieldInfo> fields = fieldInfos.Where(field => field.GetCustomAttribute<AutoWired>() != null).ToList();
            if (fields.Count != 0)
            {
                AutoWiredFiledDic.Add(type.Name, fields);
            }

        }

        public static T Resolve<T>()
        {
            object v = SingleInstanceDic[typeof(T).Name];
            return (T)v;
        }


    }



}
