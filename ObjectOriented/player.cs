namespace ObjectOriented
{
    /**
     * 玩家类 
     * 需要引入场景以便限制移动范围
     * 需要初始化玩家位置x,y
     */
    class player
    {

        Scence scence;
        int x, y;

        public player(Scence scence, int x, int y)
        {
            this.scence = scence;
            this.x = x;
            this.y = y;
        }

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }

        public bool up()
        {
            if (X > 1)
            {

                X--;
                return true;
            }
            return false;

        }
        public bool down()
        {
            if (X < scence.Row - 2)
            {
                X++;
                return true;
            }
            return false;
        }
        public bool left()
        {
            if (Y > 1)
            {
                Y--;
                return true;
            }
            return false;

        }
        public bool right()
        {
            if (Y < scence.Col - 2)
            {
                Y++;
                return true;
            }
            return false;
        }

        public bool move(char key)
        {
            if ('w' == key)
            {
                return up();
            }
            if ('a' == key)
            {
                return left();
            }
            if ('s' == key)
            {
                return down();
            }
            if ('d' == key)
            {
                return right();
            }
            return false;
        }
    }
}
