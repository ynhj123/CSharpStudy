using System;

namespace ObjectOriented
{
    /**
     *  场景类 包含长宽及打印场景方法
     */
    class Scence
    {
        public static Random random = new Random();
        //行
        private int row;
        //列
        private int col;

        public Scence(int row, int col)
        {
            this.row = row;
            this.col = col;
        }

        public int Row { get => row; set => row = value; }
        public int Col { get => col; set => col = value; }

        /**
         * 打印场景 需要传入玩家以便打印玩家坐在位置
         */
        public void Print(player player)
        {
            //清空
            Console.Clear();
            //遍历
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    //边框
                    if (i == 0 || i == row - 1 || j == 0 || j == col - 1)
                    {
                        Console.Write(" # ");
                    }
                    //玩家
                    else if (i == player.X && j == player.Y)
                    {
                        Console.Write(" 0 ");
                    }
                    else
                    {
                        Console.Write("   ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
