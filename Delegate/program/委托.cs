using System;
using System.Collections.Generic;

namespace 委托
{

    class Student
    {
        public string name;
        public int score;

        public Student(string n, int s)
        {
            name = n;
            score = s;
        }

        public override string ToString()
        {
            return string.Format("{0}:{1} ", name, score);
        }
    }

    class Program
    {
        delegate void Level();      // 函数类型    叫做委托


        delegate int Test(int a, int b, int c);

        static void LevelA()
        {
            Console.WriteLine("盖伦");
        }

        static void LevelB()
        {
            Console.WriteLine("寒冰射手");
        }

        static void LevelC()
        {
            Console.WriteLine("瑞兹");
        }

        static void LevelD()
        {
            Console.WriteLine("光辉女郎");
        }

        static int Double(int n)
        {
            return n * 2;
        }

        static int Filter(int n)
        {
            if (n < 3)
            {
                return 0;
            }
            return n;
        }


        static int CompareStudent(Student a, Student b)
        {
            if (a.score > b.score)
            {
                return -1;
            }
            if (a.score < b.score)
            {
                return 1;
            }
            return 0;
        }

        static void TestDelegate(Level level)
        {
            Console.Write("接下来调用：");
            level();
            Console.Write("调用完毕");
            return;
        }

        static void TestDelegate2(Test test)
        {
            Console.Write("接下来调用：");
            test(1, 2, 3);
            Console.Write("调用完毕");
            return;
        }

        static void TestD(int n, Level lev)
        {
            lev();
            Console.WriteLine(n);
        }

        static void Main(string[] args)
        {
            //LevelA();
            //LevelB();
            //LevelC();
            //LevelC();
            //LevelD();

            // 单播委托
            //Level level = LevelA;
            //level = LevelB;
            //level();
            //TestD(9, level);

            List<Level> levels = new List<Level>();
            levels.Add(LevelA);
            levels.Add(LevelB);
            levels.Add(LevelC);
            levels.Add(LevelD);

            //foreach (var lev in levels)
            //{
            //    lev();
            //    //Console.WriteLine(lev);
            //}

            // 多播委托
            Level lev = null;
            lev += LevelA;
            lev += LevelA;
            lev += LevelB;
            lev += LevelB;

            lev -= LevelA;
            lev -= LevelA;
            lev();

            //TestDelegate(LevelA);

            Test test = (int a, int b, int c) => { return a + b + c; };
            test(1, 2, 3);
            TestDelegate2((int a, int b, int c) => { return a + b + c; });

            Student s1 = new Student("小明", 80);
            Student s2 = new Student("小红", 90);
            Student s3 = new Student("小军", 95);

            List<Student> list = new List<Student> { s1, s2, s3 };
            // 匿名函数，lambda表达式  λ
            Comparison<Student> comp = (Student t1, Student t2) =>
            {
                return t1.name.CompareTo(t2.name);
            };
            Console.WriteLine("-=================");
            list.Sort((Student a, Student b) =>
            {
                //Console.WriteLine(s2.name);
                return a.score.CompareTo(b.score);
            });

            Console.ReadKey();
        }
    }
}
