using System;
using System.Collections.Generic;

namespace Recursion
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("打印 n-1 1-n");
            PrintNum(10);
            Console.WriteLine("普通斐波那契{0}", Fibonacci(4));
            Console.WriteLine("优化斐波那契{0}", TurningFibonacci(10));
            Console.WriteLine("打印1-n的斐波那契数列");
            Fibonacci2(10);
            Console.WriteLine("递归二分查找");
            int[] arr = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Console.WriteLine(BinarySearchElemnt(arr, 4));
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
            int right = sortElements.Length;
            return LoopBinarySearch(sortElements, left, right, element);

        }
        static int LoopBinarySearch(int[] sortElements, int left, int right, int element)
        {
            if (left > right)
            {
                throw new SystemException("值不存在");
            }
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
            return LoopBinarySearch(sortElements, left, right, element);
        }
    }
}
