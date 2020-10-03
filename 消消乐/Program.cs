using System;
using 消消乐.model;

namespace 消消乐
{
    public class Program
    {
        public static Random random = new Random();
        static void Main(string[] args)
        {
            /*Position position = new Position(1, 2);
            Position position1 = new Position(2, 1);
            Position position2 = new Position(1, 2);

            Dictionary<Position, int> dict = new Dictionary<Position, int>();
            dict[position] = 0;
            dict[position1] = 1;
            dict[position2] = 2;
            Console.WriteLine(position.Equals(position1));
            Console.WriteLine(position.Equals(position2));*/
            Canvas canvas = new Canvas();
            canvas.Run();
        }
    }
}
