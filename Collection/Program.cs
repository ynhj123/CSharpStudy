using System;

namespace Collection
{
    class Program
    {
        public static Random random;
        static void Main(string[] args)
        {
            short len = 1000;
            byte[] bytes = new byte[2];
            bytes[0] = (byte)(len % 256);
            bytes[1] = (byte)(len / 256);

            short l = (Int16)((bytes[0 + 1] << 8) | bytes[0]);

            /*PokemonManager manager = new PokemonManager();
            var p1 = new Pokemon("皮卡丘", 10);
            var p2 = new Pokemon("水箭龟", 11);
            var p3 = new Pokemon("皮卡丘", 12);
            var p4 = new Pokemon("超梦", 13);
            manager.AddPokemon(p1);
            manager.AddPokemon(p2);
            manager.AddPokemon(p3);
            manager.AddPokemon(p4);

            var list = manager.FindByName("皮卡丘");
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list[i].name + " " + list[i].level);
            }
            manager.Print();
            manager.savePath("../../pokemon.txt");
            manager.RemoveByID(2);
            manager.loadData("../../pokemon.txt");
            manager.Print();
*/

            Console.ReadKey();
            /*String a = "abcxxxx";
            String b = new String("abcxxxx");
            string c = "abcxxxx";

            Console.WriteLine(a == b);
            Console.WriteLine(a == c);
            CollectionDictary collectionDictary = new CollectionDictary();
            Dictionary<char, int> leftMaps = collectionDictary.getInitDictionary();
            Dictionary<char, int> rightMaps = collectionDictary.getInitDictionary();
            collectionDictary.removeEvenDictionary(leftMaps);
            collectionDictary.removeEvenDictionary(rightMaps);
            Dictionary<char, List<int>> dictionaries1 = collectionDictary.MargeAllDictionary(leftMaps, rightMaps);
            collectionDictary.PrintDict<char, List<int>>(dictionaries1);
            Dictionary<char, int> copyleft = collectionDictary.CopyDictionary<char, int>(leftMaps);
            collectionDictary.MargeLeftDictionary(copyleft, rightMaps);
            List<string> strList = new List<string> { "100", "200", "302", "400", "500", "601" };
          
            Dictionary<string, string> dictionaries = collectionDictary.ListToDict(strList);
            List<string> lists = collectionDictary.DictToList(dictionaries);
            List<int> orignList = new List<int> { 100, 200, 302, 400, 500, 601 };
            List<int> indexList = new List<int> { 2, 3, 5 };
            CollectionsList.RemoveList(orignList, indexList);
            foreach (var num in orignList)
            {
                Console.WriteLine(num);
            }
            //List<int> repeatArr = new List<int> { 1,2,4,5,3 };
            List<int> repeatArr = new List<int> { 5, 5, 5, 5, 5 };
            List<int> repeatLists = CollectionsList.GetRepeatList(repeatArr);
            foreach (var num in repeatLists)
            {
                Console.WriteLine(num);
            }
            int v = repeatLists.Sum();
            double v1 = repeatArr.Average();
            int v2 = repeatLists.LastIndexOf(5);
            int count = repeatLists.Count;

            repeatLists.Insert(count, v);
            int count1 = repeatLists.Count;
            repeatLists.Insert(count1, Convert.ToInt32(v1));
            int count2 = repeatLists.Count;
            repeatLists.Insert(count2, v2);//
            repeatLists.Add(v2);
            int count3 = repeatLists.Count;
            int[] vs = repeatLists.ToArray();
            PrintArr(vs);*/


            /*bool isStart = true;
            int row;
            int col;
            while (isStart)
            {
                Console.WriteLine("当前座位号");
                for (int i = 0; i < tickets.GetLength(0); i++)
                {
                    for (int j = 0; j < tickets.GetLength(1); j++)
                    {
                        string content = tickets[i, j] == 0 ? "待卖" : "已卖";
                        Console.Write("{0}_{1}:{2}; ", i + 1, j + 1, content);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("请输入座位号 如 1_2");
                string seatNumberStr = Console.ReadLine();
                string[] seatNumberStrs = seatNumberStr.Split("_");
                bool isSuccessRow = int.TryParse(seatNumberStrs[0], out row);
                bool isSuccessCol = int.TryParse(seatNumberStrs[1], out col);
                if (!isSuccessRow || !isSuccessCol)
                {
                    Console.WriteLine("输入座位信息有误！");
                }
                else
                {
                    bool isSuccess = isSellTickets(row, col);
                    if (isSuccess)
                    {
                        Console.WriteLine("购票成功！");

                    }
                    else
                    {
                        Console.WriteLine("票已卖出，请重新购买！");

                    }
                }
            }*/
            /*int[] a = { 1, 3, 5 };
            int[] b = { 2, 4 };
            PrintArr(MargeSort(b, a));*/
            /*if (random == null)
            {
                random = new Random();
            }
            for (int i = 0; i < 415; i++)
            {
                double value = random.NextDouble();
                Console.WriteLine(GetCardByProbability(value));
            }*/

            /*   int[] arr = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
               PrintArr(DoubleEvenNum(arr));
               PrintArr(BulkSortElements(arr));
               PrintArr(ReverseElements(arr));
               Console.WriteLine(BinarySearchElemnt(arr, 3));
               List<String> arrayList = new List<String>();
               arrayList.Distinct();*/
        }
        /**
         * 初始化票
         */
        static int[,] tickets = new int[3, 4];
        /**
         * 买票
         */
        static bool isSellTickets(int row, int col)
        {

            if (row > tickets.GetLength(0) || col > tickets.GetLength(1))
            {
                throw new SystemException("票不存在");
            }
            //有票
            if (tickets[row - 1, col - 1] == 0)
            {
                tickets[row - 1, col - 1] = 1;
                return true;
            }

            return false;
        }


        /**
         * 对有序数组合并排序
         */
        static int[] MargeSort(int[] a_sort_arr, int[] b_sort_arr)
        {
            int a_index = 0;
            int b_index = 0;
            int[] temp_arr = new int[a_sort_arr.Length + b_sort_arr.Length];
            int tmp_index = 0;
            //遍历数组长度次 //可拆分成3段分别循环 a和b都未越界， a越界， b越界
            while (tmp_index < temp_arr.Length)
            {    //a越界 b+
                if (a_index > (a_sort_arr.Length - 1))
                {
                    temp_arr[tmp_index] = b_sort_arr[b_index];
                    b_index++;
                }
                //b越界 a+
                else if (b_index > (b_sort_arr.Length - 1))
                {
                    temp_arr[tmp_index] = a_sort_arr[a_index];
                    a_index++;
                }
                // a大于b b + 
                else if (a_sort_arr[a_index] > b_sort_arr[b_index])
                {
                    temp_arr[tmp_index] = b_sort_arr[b_index];
                    b_index++;

                }
                //否则 a+
                else
                {
                    temp_arr[tmp_index] = a_sort_arr[a_index];
                    a_index++;
                }
                tmp_index++;
            }
            return temp_arr;
        }
        /**
         * 按权重抽卡
         */
        static String GetCardByProbability(double probability)
        {
            int[] weight = { 5, 10, 100, 100, 200 };
            string[] card = { "SSR", "SR", "R", "A", "B" };
            int weightSum = SumArray(weight);
            double lastProbalibity = 0;
            for (int i = 0; i < weight.Length; i++)
            {
                double weightProbalibity = (double)weight[i] / weightSum;
                if ((probability <= weightProbalibity + lastProbalibity))
                {
                    return card[i];
                }
                else
                {
                    lastProbalibity += weightProbalibity;
                }
            }
            return "";
        }

        /**
        * 交换数组的最大最小值
        */
        static void SwapMinAndMax(int[] arr)
        {
            int maxIndex;
            int minIndex;
            MaxElement(arr, out maxIndex);
            MinElement(arr, out minIndex);
            if (maxIndex != -1 && minIndex != -1)
            {
                SwapElement(ref arr[maxIndex], ref arr[minIndex]);
            }
        }
        /**
         * 交换2个数
         */
        static void SwapElement(ref int a, ref int b)
        {
            int tmp = a;
            a = b;
            b = tmp;
        }
        /**
         * 数组冒泡排序
         */
        static int[] BulkSortElements(int[] elements)
        {
            if (elements.Length < 2)
            {
                throw new SystemException("数组不能小于2");
            }
            //遍历elements.length-1次
            //提前判断已有序
            bool isSorted = true;
            for (int i = 0; i < elements.Length - 1; i++)
            {
                //第i次比较 -i 表示后半段已有序，无需再拍
                for (int j = 0; j < elements.Length - i - 1; j++)
                {
                    //如果elements[j]比elements[j+1]的值大，那就交换位置
                    if (elements[j] > elements[j + 1])
                    {
                        isSorted = false;
                        SwapElement(ref elements[j], ref elements[j + 1]);
                    }
                }
                if (isSorted)
                {
                    return elements;
                }

            }
            return elements;
        }
        /**
         * 数组反转
         */
        static int[] ReverseElements(int[] elements)
        {
            if (isEmpty(elements))
            {
                throw new SystemException("数组不能为空");
            }
            if (elements.Length == 1)
            {
                return elements;
            }
            int half = elements.Length / 2;
            for (int i = 0; i < half; i++)
            {
                SwapElement(ref elements[i], ref elements[elements.Length - i - 1]);
            }
            return elements;
        }
        /**
         *  二分查找 
         */
        static int BinarySearchElemnt(int[] sortElements, int element)
        {
            //设置初始值
            int left = 0;
            int right = sortElements.Length;


            while (left <= right)
            {
                //设置中间值 { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }
                int tmp = (left + right) / 2;
                if (sortElements[tmp] == element)
                {
                    return tmp;
                }
                if (sortElements[tmp] > element)
                {
                    //错位 右减
                    right = tmp - 1;
                }
                if (sortElements[tmp] < element)
                {
                    //错位 左减
                    left = tmp + 1;
                }
            }
            throw new SystemException("值不存在");
        }
        /**
         * 判断某个数是否存在数组
         */
        static bool HasElement(int[] elements, int element)
        {
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i] == element)
                {
                    return true;
                }
            }
            return false;
        }
        /**
         * 统计某个数在数组中出现次数
         */
        static int CountElement(int[] elements, int element)
        {
            int count = 0;
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i] == element)
                {
                    count++;
                }
            }
            return count;
        }
        /**
         * 数组求最大
         */
        static int MaxElement(int[] elements)
        {
            if (Program.isEmpty(elements))
            {
                throw new SystemException("数组为空");
            }
            int max = elements[0];
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i] > max)
                {
                    max = elements[i];
                }
            }
            return max;
        }

        static int MaxElement(int[] elements, out int index)
        {
            index = -1;
            if (Program.isEmpty(elements))
            {
                throw new SystemException("数组为空");
            }
            int max = elements[0];
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i] > max)
                {
                    index = i;
                    max = elements[i];
                }
            }
            return max;
        }
        /**
         * 数组求最小
         */
        static int MinElement(int[] elements)
        {
            if (Program.isEmpty(elements))
            {
                throw new SystemException("数组为空");
            }
            int min = elements[0];
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i] < min)
                {
                    min = elements[i];
                }
            }
            return min;
        }
        static int MinElement(int[] elements, out int index)
        {
            if (Program.isEmpty(elements))
            {
                throw new SystemException("数组为空");
            }
            index = -1;
            int min = elements[0];
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i] < min)
                {
                    min = elements[i];
                    index = -1;
                }
            }
            return min;
        }
        /**
         * 数组求和
         */
        static int SumArray(int[] elements)
        {
            int sum = 0;
            for (int i = 0; i < elements.Length; i++)
            {
                sum += elements[i];
            }
            return sum;
        }
        /**
         * 数组求平均
         */
        static float AverageArray(int[] elements)
        {
            int sum = SumArray(elements);
            return (float)sum / elements.Length;
        }
        /**
         * 数组偶数求2倍
         */
        static int[] DoubleEvenNum(int[] elements)
        {
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i] % 2 == 0)
                {
                    elements[i] *= 2;
                }

            }
            return elements;
        }
        /**
         * 打印数组
         */
        static void PrintArr(int[] elements)
        {
            for (int i = 0; i < elements.Length; i++)
            {
                Console.Write(elements[i] + ",");

            }
            Console.WriteLine();
        }
        /**
         * 数组是否为空
         */
        static bool isEmpty(int[] elements)
        {
            return elements == null || elements.Length == 0;
        }
    }

}
