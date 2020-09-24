using ConsoleGame.model;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleGame.Controller
{
    class AutoAttachIntervalSystem : IExecuteSystem
    {
        List<Player> autoPlayer = new List<Player>();
        GameSence scence;

        public AutoAttachIntervalSystem(GameSence scence)
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
            List<Sprite> players = scence.sprites.Where(spirte => spirte is Player).ToList();
            foreach (Player player in players)
            {
                if (autoPlayer.Contains(player) && player.AttchInterval == 5)
                {
                    MsgAttack msgAttack = new MsgAttack();
                    msgAttack.playId = player.Id;
                    NetManagerEvent.Send(msgAttack);
                    player.AttchInterval = 1;

                }
                else
                {
                    if (player.AttchInterval < 5)
                    {
                        player.AttchInterval += 1;

                    }

                }
            }
        }
    }
}
