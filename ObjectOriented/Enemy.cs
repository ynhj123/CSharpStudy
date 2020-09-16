namespace ObjectOriented
{
    class Enemy : Character
    {
        public Enemy(string name, int hp, int mp, int attach, int spell)
        {
            this.Name = name;
            this.Hp = hp;
            this.Mp = mp;
            this.Attach = attach;
            this.Spell = spell;

        }

        public void randomHandle(Character other)
        {
            int handleInt = Scence.random.Next(1, 11);
            if (handleInt <= 6)
            {
                AttachOther(other);
            }
            else if (handleInt <= 9)
            {
                Skill1(other);
            }
            else
            {
                Skill2(other);

            }
            this.Print(this);
            this.Print(other);

        }

        private void Skill1(Character other)
        {
            int offset = Scence.random.Next(-2, 3);
            Skill skill = new Skill("斩钢闪", 1, this.Spell + offset);
            if (!skill.AttachOther(this, other))
            {
                this.AttachOther(other);
            }
        }
        private void Skill2(Character other)
        {
            int offset = Scence.random.Next(3, 5);
            Skill skill = new Skill("狂风绝息斩", 10, this.Spell * offset);
            if (!skill.AttachOther(this, other))
            {
                this.AttachOther(other);
            }

        }



    }
}
