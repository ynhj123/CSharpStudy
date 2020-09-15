using System;
using System.Collections.Generic;

namespace Recursion
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("统计字符串出现次数");
            Dictionary<char, int> dictionaries = Statistics("asxax zsxz");
            int[] intArr = StatisticsByIntArr("asxax zsxz");
            for (int i = 0; i < intArr.Length; i++)
            {
                if (intArr[i] != 0)
                {
                    Console.WriteLine("{0}:{1}", (char)i, intArr[i]);

                }
            }
            PrintDict<char, int>(dictionaries);
            Console.WriteLine("打印杨辉三角");
            PrintYHtriangle(10, 10);
            Console.ReadKey();
            Console.WriteLine("打印 n-1 1-n");
            PrintNum(10);
            Console.WriteLine("普通斐波那契{0}", Fibonacci(4));
            Console.WriteLine("优化斐波那契{0}", TurningFibonacci(10));
            Console.WriteLine("打印1-n的斐波那契数列");
            Fibonacci2(10);
            Console.WriteLine("递归二分查找");
            int[] arr = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Console.WriteLine(BinarySearchElemnt(arr, 9));
        }
        /**
         * 统计字符串出现次数
         */
        static Dictionary<char, int> Statistics(string str)
        {
            str = str.Replace(" ", "");
            Dictionary<char, int> maps = new Dictionary<char, int>();
            int[] statis = new int[126];

            for (int i = 0; i < str.Length; i++)
            {
                statis[(int)str[i]]++;
                if (maps.ContainsKey(str[i]))
                {
                    maps[str[i]]++;
                }
                else
                {
                    maps.Add(str[i], 1);
                }

            }
            return maps;
        }
        static int[] StatisticsByIntArr(string str)
        {
            // ASCII 128 2^8  中文 65536 2^32 utf-8 (中文3位) 或者 utf-16(中文2位 对纯中文支持更好)
            int[] statis = new int[128];
            //桶排序 （存在问题：桶太多）
            for (int i = 0; i < str.Length; i++)
            {
                statis[str[i]]++;

            }
            return statis;
        }
        /**
         *  打印字典
         */
        static void PrintDict<K, V>(Dictionary<K, V> dict)
        {
            foreach (KeyValuePair<K, V> kvp in dict)
            {
                Console.WriteLine("key：{0},value：{1}", kvp.Key, kvp.Value);
            }
        }
        /**
         * 打印杨辉三角
         * 第n行的m个数可表示为 C(n-1，m-1)，即为从n-1个不同元素中取m-1个元素的组合数。
         * 每个数字等于上一行的左右两个数字之和。可用此性质写出整个杨辉三角。即第n+1行的第i个数等于第n行的第i-1个数和第i个数之和，这也是组合数的性质之一。
         * 即 C(n+1,i)=C(n,i)+C(n,i-1)。
         */
        static void PrintYHtriangle(int row, int col)
        {
            int[,] nums = new int[row, col];
            for (int i = 0; i < row; i++)
            {
                nums[i, 0] = 1;
            }
            for (int i = 1; i < row; i++)
            {
                for (int j = 1; j < col; j++)
                {
                    nums[i, j] = nums[i - 1, j] + nums[i - 1, j - 1];
                }
            }
            for (int i = 0; i < nums.GetLength(0); i++)
            {
                for (int j = 0; j < i + 1; j++)
                {
                    Console.Write("{0}   ", nums[i, j]);
                }
                Console.WriteLine();
            }
        }
        /**
         *  正反打印1 - n
         */
        static void PrintNum(int num)
        {
            //从大到小

            Console.WriteLine(num--);

            if (num == 0)
            {
                return;
            }

            PrintNum(num);
            Console.WriteLine(num + 1);
            //从小到大
        }
        /**
         * 容器 优化斐波那契查询速率
         */
        static Dictionary<int, int> maps = new Dictionary<int, int>(){
            {1,1 },{2,1}
            };
        /**
         * 普通斐波那契
         */
        static int Fibonacci(int n)
        {
            if (n == 1)
            {
                return 1;
            }
            if (n == 2)
            {
                return 1;
            }

            return Fibonacci(n - 1) + Fibonacci(n - 2);

        }
        /**
         * 容器优化斐波那契
         */
        static int TurningFibonacci(int n)
        {
            //优化
            bool isContains = maps.ContainsKey(n);
            if (!isContains)
            {
                int value = TurningFibonacci(n - 1)
                   + TurningFibonacci(n - 2);
                maps.Add(n, value);

            }
            return maps[n];

        }
        /**
         * 打印全部1-n的斐波那契
         */
        static int Fibonacci2(int n)
        {
            int left = 0;
            if (n == 0)
            {
                Console.WriteLine(0);
                return 0;
            }
            int tmp = 1;
            if (n == 1)
            {
                Console.WriteLine(1);
                return 1;
            }
            int right = 0;
            Console.WriteLine(1);
            for (int i = 2; i <= n; i++)
            {

                right = left + tmp;
                Console.WriteLine(right);
                left = tmp;
                tmp = right;
            }
            return right;
        }
        /**
         *  递归版二分查找
         */
        static int BinarySearchElemnt(int[] sortElements, int element)
        {
            //设置初始值
            int left = 0;
            int right = sortElements.Length - 1;
            return LoopBinarySearch(sortElements, left, right, element);

        }
        static int LoopBinarySearch(int[] sortElements, int left, int right, int element)
        {
            if (left > right)
            {
                throw new SystemException("值不存在");
            }
            // 加法可能存在int越界
            //int tmp = (left + right) / 2;
            //加差数
            int tmp = left + (right - left) / 2;
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
                //错位 左加
                left = tmp + 1;
            }
            return LoopBinarySearch(sortElements, left, right, element);
        }
    }
}
