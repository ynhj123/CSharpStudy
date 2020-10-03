using System;
using System.Collections.Generic;
using System.Linq;

namespace 消消乐.model
{
    class Canvas
    {
        int maxX = 10;
        int maxY = 10;
        int origRow;
        int origCol;

        Dictionary<Position, Node> nodeDict = new Dictionary<Position, Node>();
        List<Node> ReducesNodes;
        Dictionary<int, List<Node>> cacheDepartDestoryNodes;
        public void Load()
        {
            origRow = Console.CursorTop;
            origCol = Console.CursorLeft;


            for (int i = 0; i < maxX; i++)
            {
                for (int j = 0; j < maxY; j++)
                {
                    Node node = NodeFactory.CreateNode(i, j);
                    nodeDict.Add(node.Position, node);
                }
            }
        }
        public void Run()
        {
            Load();
            while (true)
            {

                ReducesNodes = new List<Node>(maxX * maxY);
                FindReduceRowAndCol();
                //判断是否存在扩散点
                if (IsStop())
                {
                    //打印
                    Print();
                    Console.WriteLine("请输入一个坐标a,b");
                    string postionStr = Console.ReadLine();
                    string[] postionArr = postionStr.Split(',');

                    Position position = new Position(int.Parse(postionArr[0]), int.Parse(postionArr[1]));

                    Console.WriteLine("请按上下左右移动");
                    ConsoleKey key = Console.ReadKey().Key;
                    Position goal = new Position(position.X, position.Y);
                    switch (key)
                    {
                        case ConsoleKey.UpArrow:
                            goal.Y--;
                            break;
                        case ConsoleKey.DownArrow:
                            goal.Y++;
                            break;
                        case ConsoleKey.LeftArrow:
                            goal.X--;
                            break;
                        case ConsoleKey.RightArrow:
                            goal.X++;
                            break;
                        default:
                            break;
                    }
                    position = SwapNodeByPostion(position, goal);

                    //用户修改操作
                }
                else
                {
                    List<Node> tmpReduces = new List<Node>();
                    //递归扩散
                    foreach (var item in ReducesNodes)
                    {
                        FindAround(item, tmpReduces);
                    }
                    foreach (var item in tmpReduces)
                    {
                        ReducesNodes.Add(item);
                    }
                    //数据消除
                    //数据移动
                    //按列移动
                   
                    foreach (var item in ReducesNodes)
                    {
                        Position position = item.Position;

                        while (position.Y > 0)
                        {
                            Position goal = new Position(position.X, position.Y-1);
                            position =  SwapNodeByPostion(position, goal);
                        }
                    }
                    //数据填充
                    foreach (var item in ReducesNodes)
                    {
                        item.Type = NodeFactory.RoundType();
                        item.Status = Status.stop;
                    }
                }
            }


        }

        private Position SwapNodeByPostion(Position position, Position goal)
        {
            Node tmp = nodeDict[position];
            tmp.Position = goal;
            Node goalNode = nodeDict[goal];
            goalNode.Position = position;
            nodeDict[position] = goalNode;
            nodeDict[goal] = tmp;
            return goal;
        }

        private void Print()
        {
            Console.Clear();
            for (int i = 0; i < maxX; i++)
            {
                for (int j = 0; j < maxY; j++)
                {
                    Node node = GetNodeByPostion(i, j);
                    WriteAt("*", j, i, SwitchColor(node));
                }
            }
            
            Console.WriteLine();
        }

        private ConsoleColor SwitchColor(Node node)
        {
            if (Type.blue == node.Type)
            {
                return ConsoleColor.Blue;
            }
            if (Type.red == node.Type)
            {
                return ConsoleColor.Red;
            }
            if (Type.yello == node.Type)
            {
                return ConsoleColor.Yellow;
            }
            if (Type.green == node.Type)
            {
                return ConsoleColor.Green;
            }
            if (Type.gray == node.Type)
            {
                return ConsoleColor.Gray;
            }
            if (Type.purple == node.Type)
            {
                return ConsoleColor.Magenta;
            }
            if (Type.orign == node.Type)
            {
                return ConsoleColor.Cyan;
            }
            else
            {
                return ConsoleColor.White;
            }
        }

        void WriteAt(string s, int x, int y, ConsoleColor color = ConsoleColor.White)
        {
            try
            {
                Console.ForegroundColor = color;
                Console.SetCursorPosition(origCol + y, origRow + x);
                Console.Write(s);
                Console.ResetColor();
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }

        private void FindAround(Node node, List<Node> tmpReduces)
        {


            //相同判定
            Node top = GetNodeByPostion(node.Position.X, node.Position.Y - 1);
            if (top != null && Status.reduce != top.Status && top.Equals(node))
            {
                top.Status = Status.reduce;
                tmpReduces.Add(top);
                FindAround(top, tmpReduces);
            }

            Node bottom = GetNodeByPostion(node.Position.X, node.Position.Y + 1);
            if (bottom != null && Status.reduce != bottom.Status && bottom.Equals(node))
            {
                bottom.Status = Status.reduce;
                tmpReduces.Add(bottom);
                FindAround(bottom, tmpReduces);
            }
            Node left = GetNodeByPostion(node.Position.X - 1, node.Position.Y);

            if (left != null && Status.reduce != left.Status && left.Equals(node))
            {
                left.Status = Status.reduce;
                tmpReduces.Add(left);
                FindAround(left, tmpReduces);
            }
            Node right = GetNodeByPostion(node.Position.X + 1, node.Position.Y);
            if (right != null && Status.reduce != right.Status && right.Equals(node))
            {
                right.Status = Status.reduce;
                tmpReduces.Add(right);
                FindAround(right, tmpReduces);
            }
        }
        /// <summary>
        /// 判断是否全部停止,如果没有则存在扩散点
        /// </summary>
        /// <returns></returns>
        private bool IsStop()
        {

            int count = nodeDict.Where(spir => spir.Value.Status != Status.stop).Count();
            return count == 0;
        }

        /// <summary>
        /// 寻找扩散点
        /// </summary>
        private void FindReduceRowAndCol()
        {
            FindReduceRow();
            FindReduceCol();
        }

        private void FindReduceRow()
        {

            for (int y = 0; y < maxY; y++)
            {
                int col = 0;
                while (col < maxX - 2)
                {
                    Node left = GetNodeByPostion(col, y);
                    Node mid = GetNodeByPostion(col + 1, y);
                    Node right = GetNodeByPostion(col + 2, y);

                    //判断 三个全相同
                    if (left.Equals(mid) && mid.Equals(right))
                    {
                        left.Status = Status.reduce;
                        mid.Status = Status.reduce;
                        right.Status = Status.reduce;
                        ReducesNodes.Add(left);
                        ReducesNodes.Add(mid);
                        ReducesNodes.Add(right);
                        col += 3;
                    }

                    //只有中间和右边的相同
                    else if (mid.Equals(right))
                    {
                        col += 1;
                    }
                    //其它
                    else
                    {
                        col += 2;
                    }
                }
            }

        }

        private void FindReduceCol()
        {

            for (int x = 0; x < maxX; x++)
            {
                int row = 0;
                while (row < maxY - 2)
                {
                    Node left = GetNodeByPostion(x, row);
                    Node mid = GetNodeByPostion(x, row + 1);
                    Node right = GetNodeByPostion(x, row + 2);
                    //判断 三个全相同
                    if (left.Equals(mid) && mid.Equals(right))
                    {
                        left.Status = Status.reduce;
                        mid.Status = Status.reduce;
                        right.Status = Status.reduce;
                        ReducesNodes.Add(left);
                        ReducesNodes.Add(mid);
                        ReducesNodes.Add(right);
                        row += 3;
                    }
                    //只有中间和右边的相同
                    else if (mid.Equals(right))
                    {
                        row += 1;
                    }
                    //其它
                    else
                    {
                        row += 2;
                    }
                }
            }

        }

        private Node GetNodeByPostion(int x, int y)
        {
            //越界为null
            if (x < 0 || y < 0 || x >= maxX || y >= maxY)
            {
                return null;
            }
            return nodeDict[new Position(x, y)];
        }
    }
}
