using System;

namespace ObjectOriented
{
    class Skill
    {
        string name;
        int mp;
        int hurt;

        public Skill(string name, int mp, int hurt)
        {
            this.name = name;
            this.mp = mp;
            this.hurt = hurt;
        }

        public string Name { get => name; set => name = value; }
        public int Mp { get => mp; set => mp = value; }
        public int Hurt { get => hurt; set => hurt = value; }


        public bool AttachOther(Character character, Character other)
        {
            if (character.Mp < this.mp)
            {
                Console.WriteLine("蓝量不足！");
                return false;
            }
            character.Mp -= this.mp;
            other.Hp -= this.hurt;
            Console.WriteLine("{0}使用{1}攻击了{2}，造成了{3}点伤害", character.Name, this.name, other.Name, this.hurt);
            return true;
        }
    }
}
