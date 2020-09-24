using ConsoleGame.model;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleGame.Controller
{

    class CollisionSystem : IExecuteSystem
    {
        GameSence scence;

        public CollisionSystem(GameSence scence)
        {
            this.scence = scence;
        }

        public void Execute()
        {
            var sprites = scence.sprites;
            //碰撞监听
            List<Sprite> players = sprites.Where(sprite => sprite.GetType() == typeof(Player)).ToList();
            List<Sprite> skills = sprites.Where(sprite => sprite.GetType() == typeof(Skill)).ToList();
            for (int i = players.Count - 1; i >= 0; i--)
            {
                for (int j = skills.Count - 1; j >= 0; j--)
                {
                    if (players[i].Position.X == skills[j].Position.X && players[i].Position.Y == skills[j].Position.Y)
                    {
                        SpriteDestorySystem spriteDestorySystem = SpriteDestorySystem.GetSpriteDestorySystem();
                        spriteDestorySystem.sprites.Enqueue(players[i]);

                    }
                }

            }
        }
    }
}
