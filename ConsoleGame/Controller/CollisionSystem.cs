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
            Player player = (Player)sprites.Where(sprite => sprite.GetType() == typeof(Player) && sprite.Id == ScenceController.user.Userid).FirstOrDefault();
            if(player!= null)
            {
                List<Sprite> skills = sprites.Where(sprite => sprite.GetType() == typeof(Skill)).ToList();
                for (int j = skills.Count - 1; j >= 0; j--)
                {
                    if (player.Position.X == skills[j].Position.X && player.Position.Y == skills[j].Position.Y)
                    {
                        SpriteDestorySystem spriteDestorySystem = SpriteDestorySystem.GetSpriteDestorySystem();
                        spriteDestorySystem.sprites.Enqueue(player);
                        MsgLeave msgLeave = new MsgLeave
                        {
                            playId = player.Id
                        };
                        NetManagerEvent.Send(msgLeave);

                    }
                }
            }
            

        }
    }
}

