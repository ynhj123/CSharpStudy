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
            ConsoleKey key = ConsoleKey.Z;
            while (Console.KeyAvailable)
            {
                key = Console.ReadKey(true).Key;

            }
            handleKey(key);
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
                    if (player.AttchInterval == 5)
                    {
                        MsgAttack msgAttack = new MsgAttack();
                        msgAttack.playId = player.Id;
                        NetManagerEvent.Send(msgAttack);
                        player.AttchInterval = 1;
                        ;
                    }
                    break;
                default:
                    break;
            }


        }


    }
}
