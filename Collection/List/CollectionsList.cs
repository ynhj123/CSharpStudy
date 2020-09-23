using System.Collections.Generic;

namespace Collection.List
{
    class CollectionsList
    {
        public static void RemoveList(List<int> orignList, List<int> removeIndexs)
        {
            //顺序删除
            removeIndexs.Sort();
            for (int i = removeIndexs.Count - 1; i >= 0; i--)
            {
                orignList.RemoveAt(removeIndexs[i]);
            }
        }

        public static List<int> GetRepeatList(List<int> orignList)
        {
            Dictionary<int, int> maps = new Dictionary<int, int>();
            foreach (var num in orignList)
            {
                int value;
                bool isSuccess = maps.TryGetValue(num, out value);
                if (isSuccess)
                {
                    maps[num] += 1;
                }
                else
                {
                    maps.Add(num, 1);
                }
            }
            List<int> newList = new List<int>();
            foreach (var num in orignList)
            {
                if (maps[num] != 1)
                {
                    newList.Add(num);
                }
            }
            return newList;
        }
    }
}
