using System;

namespace ConsoleGame.Component
{
    public class VelocityComponent : IComponent
    {
        private Veloctity veloctity;

        internal Veloctity Veloctity { get => veloctity; set => veloctity = value; }
    }

    public enum Veloctity
    {
        left = ConsoleKey.LeftArrow,
        right = ConsoleKey.RightArrow,
        up = ConsoleKey.UpArrow,
        down = ConsoleKey.DownArrow,
    }

}
