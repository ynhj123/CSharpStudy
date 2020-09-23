using OrderAndAccountBook;
using System;
using System.Collections.Generic;

namespace Seriallize
{
    class Program
    {
        static void ShowOrder(AccountBook book, int id)
        {
            OrderForm order = book.GetOrder(id);
            if (order != null)
            {
                Console.WriteLine(order);
            }
            else
            {
                Console.WriteLine("没有找到订单ID " + id);
            }
        }

        static void ShowOrder(AccountBook book, string name)
        {
            List<OrderForm> l = book.GetOrder(name);
            if (l == null || l.Count == 0)
            {
                Console.WriteLine("没有找到订单Name " + name);
                return;
            }

            for (int i = 0; i < l.Count; ++i)
            {
                Console.Write(l[i]);
                Console.Write("   ");
            }
            Console.WriteLine();
        }

        static void TestSave()
        {
            OrderForm order1 = new OrderForm("冰激凌", 10.0, 1);
            OrderForm order2 = new OrderForm("苹果", 1.0, 2);
            OrderForm order3 = new OrderForm("香蕉", 0.8, 5);
            OrderForm order4 = new OrderForm("香蕉", 0.8, 3);
            OrderForm order5 = new OrderForm("冰激凌", 10.0, 3);

            AccountBook book = new AccountBook();
            book.AddOrder(order1);
            book.AddOrder(order2);
            book.AddOrder(order3);
            book.AddOrder(order4);
            book.AddOrder(order5);

            //ShowOrder(book, "冰激凌");
            //ShowOrder(book, "香蕉");
            //ShowOrder(book, "芝麻");

            //book.SaveToFile();
            //book.SaveToFile_Stream();
            book.WriteToFile();
        }

        static void TestLoad()
        {
            AccountBook book = new AccountBook();
            //book.LoadFromFile();
            //book.LoadFromFile_Stream();
            book.LoadFile();
            book.PrintAll();
        }

        static void Main(string[] args)
        {
            TestSave();
            //TestLoad();

            Console.ReadKey();
        }
    }
}
