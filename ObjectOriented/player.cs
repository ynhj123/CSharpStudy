namespace ObjectOriented
{
    /**
     * 玩家类 
     * 需要引入场景以便限制移动范围
     * 需要初始化玩家位置x,y
     */
    class player : Character
    {

        Scence scence;
        int x, y;



        public player(string name, int hp, int mp, int attach, int spell)
        {
            this.Name = name;
            this.Hp = hp;
            this.Mp = mp;
            this.Attach = attach;
            this.Spell = spell;
        }
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
        public void Handle(char key, Character other)
        {
            if ('a' == key)
            {
                AttachOther(other);
            }
            if ('q' == key)
            {
                Skill1(other);
            }
            if ('r' == key)
            {
                Skill2(other);
            }
            this.Print(this);
            this.Print(other);


        }
        private void Skill1(Character other)
        {
            int offset = Scence.random.Next(-2, 3);
            Skill skill = new Skill("错玉切", 1, this.Spell + offset);
            if (!skill.AttachOther(this, other))
            {
                this.AttachOther(other);
            }
        }
        private void Skill2(Character other)
        {
            int offset = Scence.random.Next(3, 5);
            Skill skill = new Skill("封尘绝念斩", 10, this.Spell * offset);
            if (!skill.AttachOther(this, other))
            {
                this.AttachOther(other);
            }
        }
    }
}
