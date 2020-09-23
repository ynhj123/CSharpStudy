using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Collection.List
{
    class CollectionDictary
    {
        /**
         * 初始化一个字典
         */
        public Dictionary<char, int> getInitDictionary()
        {
            if (Program.random == null)
            {
                Program.random = new Random();
            }

            Dictionary<char, int> maps = new Dictionary<char, int>();
            for (char i = 'a'; i <= 'z'; i++)
            {
                maps.Add(i, Program.random.Next(1, 11));
            }
            return maps;
        }
        /**
         * 删除字典中的偶数
         */
        public void removeEvenDictionary(Dictionary<char, int> maps)
        {

            List<char> keys = maps.Where(pair => pair.Value % 2 == 0).Select(pair => pair.Key).ToList();
            foreach (var key in keys)
            {
                maps.Remove(key);
            }
        }
        /**
         * 合并2个字典，左边有的就不用加了
         */
        public void MargeLeftDictionary(Dictionary<char, int> leftMaps, Dictionary<char, int> rightMaps)
        {

            List<KeyValuePair<char, int>> lists = rightMaps.Where(pair => !leftMaps.ContainsKey(pair.Key)).ToList();
            foreach (var item in lists)
            {
                leftMaps.Add(item.Key, item.Value);
            }
        }
        public Dictionary<char, List<int>> MargeAllDictionary(Dictionary<char, int> leftMaps, Dictionary<char, int> rightMaps)
        {
            Dictionary<char, List<int>> dict = new Dictionary<char, List<int>>();
            foreach (var item in leftMaps)
            {
                SetValue(dict, item);

            }
            foreach (var item in rightMaps)
            {
                SetValue(dict, item);
            }

            return dict;
        }

        private static void SetValue(Dictionary<char, List<int>> dict, KeyValuePair<char, int> item)
        {
            List<int> lists;
            bool isSuccess = dict.TryGetValue(item.Key, out lists);
            if (isSuccess)
            {
                lists.Add(item.Value);
            }
            else
            {
                lists = new List<int>();
                lists.Add(item.Value);
                dict.Add(item.Key, lists);
            }
        }

        public Dictionary<K, V> CopyDictionary<K, V>(Dictionary<K, V> maps)
        {

            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter Formatter =
                 new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter(null, new System.Runtime.Serialization.StreamingContext(System.Runtime.Serialization.StreamingContextStates.Clone));
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            Formatter.Serialize(stream, maps);
            stream.Position = 0;
            Dictionary<K, V> clonedObj = (Dictionary<K, V>)Formatter.Deserialize(stream);
            stream.Close();
            return clonedObj;
        }
        //将一个list 第一位做key,第二位做value 转成map
        public Dictionary<string, string> ListToDict(List<String> list)
        {
            Dictionary<string, string> maps = new Dictionary<string, string>();
            for (int i = 1; i < list.Count; i += 2)
            {
                maps.Add(list[i - 1], list[i]);
            }
            return maps;
        }
        //将map的key value 转回 list
        public List<String> DictToList(Dictionary<string, string> maps)
        {
            List<string> list = new List<string>();
            foreach (var pair in maps)
            {
                list.Add(pair.Key);
                list.Add(pair.Value);
            }
            return list;
        }

        public void PrintDict<K, V>(Dictionary<K, V> maps)
        {
            Console.Write("{");
            foreach (var item in maps)
            {
                if (typeof(ICollection).IsAssignableFrom(item.Value.GetType()))
                {
                    ICollection collection = (ICollection)item.Value;
                    StringBuilder val = new StringBuilder();

                    val.Append("[");
                    foreach (var v in collection)
                    {
                        val.Append(v);
                        val.Append(", ");
                    }
                    val.Remove(val.Length - 2, 2);
                    val.Append("]");
                    Console.Write("({0}:{1})", item.Key, val);

                }
                else
                {
                    Console.Write("({0}:{1})", item.Key, item.Value);

                }
            }
            Console.Write("}");
        }

    }
}
