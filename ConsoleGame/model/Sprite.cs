using ConsoleGame.Component;
using System;

namespace ConsoleGame.model
{
    public abstract class Sprite
    {
        private char style;
        private ConsoleColor color;
        bool isMove;
        private string id;


        private PositionComponent position = new PositionComponent();
        private VelocityComponent velocity = new VelocityComponent();

        public PositionComponent Position { get => position; set => position = value; }
        public VelocityComponent Velocity { get => velocity; set => velocity = value; }
        public char Style { get => style; set => style = value; }
        public bool IsMove { get => isMove; set => isMove = value; }
        public string Id { get => id; set => id = value; }
        public ConsoleColor Color { get => color; set => color = value; }

        public abstract bool Move(GameSence scence);



    }
}
