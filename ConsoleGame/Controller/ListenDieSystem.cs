using ConsoleGame.model;
using System.Linq;

namespace ConsoleGame.Controller
{
    class ListenDieSystem : IExecuteSystem
    {
        private Player player;
        private GameSence gameSence;

        public ListenDieSystem(Player player, GameSence gameSence)
        {
            this.player = player;
            this.gameSence = gameSence;
        }

        public void Execute()
        {
            if (!gameSence.sprites.Contains(player))
            {

                gameSence.isStrat = false;
                gameSence.isWin = false;

            }
            else if (gameSence.sprites.Where(sprite => sprite.GetType() == typeof(Player)).ToList().Count == 1)
            {

                gameSence.isStrat = false;
                gameSence.isWin = true;


            }
        }
    }
}
