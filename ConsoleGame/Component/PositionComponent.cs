namespace ConsoleGame.Component
{
    public class PositionComponent : IComponent
    {
        int x;
        int y;

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
    }
}
