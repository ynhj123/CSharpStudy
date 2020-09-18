using ConsoleGame.model;
using System;

namespace ConsoleGame.Controller
{
    class KeywordSystem : IExecuteSystem
    {
        Player player;
        GameSence sence;

        public KeywordSystem(Player player, GameSence sence)
        {
            this.player = player;
            this.sence = sence;
        }

        public void Execute()
        {
            while (Console.KeyAvailable)
            {
                handleKey(Console.ReadKey(true).Key);

            }
        }

        private void handleKey(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    player.Velocity.Veloctity = Component.Veloctity.up;
                    player.IsMove = true;
                    break;
                case ConsoleKey.DownArrow:
                    player.Velocity.Veloctity = Component.Veloctity.down;
                    player.IsMove = true;
                    break;
                case ConsoleKey.LeftArrow:
                    player.Velocity.Veloctity = Component.Veloctity.left;
                    player.IsMove = true;
                    break;
                case ConsoleKey.RightArrow:
                    player.Velocity.Veloctity = Component.Veloctity.right;
                    player.IsMove = true;
                    break;
                case ConsoleKey.Spacebar:
                    player.IsMove = false;
                    break;
                case ConsoleKey.A:
                    player.attach(sence);
                    break;
                default:
                    break;
            }

        }


    }
}
