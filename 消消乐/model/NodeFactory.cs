using System;

namespace 消消乐.model
{
    class NodeFactory
    {
        static int[] cache = new int[2];
        public static Node CreateNode(int x, int y)
        {


            Node node = new Node();
            node.Type = RoundType();
            node.Status = Status.stop;

            node.Position = new Position(x, y);

            return node;
        }

        public static Type RoundType()
        {
            int type = Program.random.Next(0, 7);
            //如果随机数连续出现3次则重随
            if (cache[0] == type)
            {
                if (cache[1] > 3)
                {
                    //重随
                    while (type != cache[0])
                    {
                        type = Program.random.Next(0, 7);

                    }
                    cache[0] = type;
                    cache[1] = 1;
                }
                else
                {
                    cache[1]++;
                }

            }
            else
            {
                cache[0] = type;
                cache[1] = 1;
            }

            return Enum.Parse<Type>(type.ToString()); ;
        }
    }
}
