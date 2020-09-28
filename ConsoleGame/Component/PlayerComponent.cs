namespace ConsoleGame.Component
{
    class PlayerComponent : IComponent
    {
        string id;
        int hp;
        int attachInterval = 3;
        int mp;
        int resistance;

        public int Hp { get => hp; set => hp = value; }
        public int AttachInterval { get => attachInterval; set => attachInterval = value; }
        public string Id { get => id; set => id = value; }
        public int Mp { get => mp; set => mp = value; }
        public int Resistance { get => resistance; set => resistance = value; }
    }
}
