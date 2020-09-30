using System;
using System.Collections.Generic;
using System.Text;

namespace 消消乐.model
{
    class NodeFactory
    {
        int[] cache = new int[2];
        public Node CreateNode()
        {
            int type = Program.random.Next(0, 7);
            //如果随机数连续出现3次则重随
            if(cache[0] == type)
            {
                if(cache[1] > 3)
                {
                    //重随
                    while(type != cache[0])
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

            Node node = new Node();
            node.Type = Enum.Parse<Type>(type.ToString());
            node.Status = Status.stop;
            return node;
        }
    }
}
