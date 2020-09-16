using System;

namespace ObjectOriented
{
    class Character
    {
        string name;
        int hp;
        int mp;
        int attach;
        int spell;

        public int Hp { get => hp; set => hp = value; }
        public int Mp { get => mp; set => mp = value; }
        public int Attach { get => attach; set => attach = value; }
        public int Spell { get => spell; set => spell = value; }
        public string Name { get => name; set => name = value; }

        public void AttachOther(Character other)
        {
            int hurt = Scence.random.Next(-2, 3) + this.attach;
            other.hp -= hurt;
            Console.WriteLine("{0}攻击了{1}，造成了{2}点伤害", this.Name, other.Name, hurt);
        }
        public bool IsSurvive(Character character)
        {
            return character.hp > 0;

        }
        public void Print(Character other)
        {
            Console.WriteLine("{0}:血量{1}，蓝量{2}", other.name, other.hp, other.mp);
        }
        public void Print()
        {
            Console.WriteLine("{0}:血量{1}，蓝量{2}", this.name, this.hp, this.mp);
        }
    }
}
