using ConsoleGame.model;
using System.Collections.Generic;

namespace ConsoleGame.Controller
{
    class AutoAttachSystem : IExecuteSystem
    {
        List<Player> autoPlayer = new List<Player>();
        GameSence scence;
        int attchInterval = 3;
        public AutoAttachSystem(GameSence scence)
        {
            this.scence = scence;
        }
        public void AddAutoPlayer(Player player)
        {
            autoPlayer.Add(player);
        }
        public void RemoveAutoPlayer(Player player)
        {
            autoPlayer.Remove(player);
        }
        public void Execute()
        {

            foreach (Player player in autoPlayer)
            {
                if (scence.sprites.Contains(player) && attchInterval == 3)
                {
                    player.attach(this.scence);
                    attchInterval = 1;

                }
                else
                {
                    attchInterval += 1;

                }
            }
        }
    }
}
