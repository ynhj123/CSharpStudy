using System;

namespace test2
{
    class Program
    {
        private static void
            Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            float a = (float)1.0;

            float b = 1;
            int v = 1;
            ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
            string v1 = Console.ReadLine();
            string v2 = Console.ReadLine();
            test test = new test();
            test.Username = "1";


        }


    }
    class test
    {
        private String username;
        private String password;

        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
    }
}
