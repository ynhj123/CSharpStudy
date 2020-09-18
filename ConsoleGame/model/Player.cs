using ConsoleGame.Component;

namespace ConsoleGame.model
{
    public class Player : Sprite
    {
        PlayerComponent player = new PlayerComponent();
        public Player(int hp, int x, int y, char style)
        {
            this.player.Hp = hp;
            this.Position.X = x;
            this.Position.Y = y;
            this.Velocity.Veloctity = Veloctity.right;
            this.Style = style;
            this.IsMove = false;

        }

        public void attach(GameSence scence)
        {
            Skill skill = new Skill(10, this.Position, this.Velocity.Veloctity);
            skill.Style = '*';
            scence.AddSprite(skill);
        }
        public void beAttacked(Skill skill, GameSence scence)
        {
            this.player.Hp -= skill.Damage;
            if (player.Hp <= 0)
            {
                this.destory(scence);
            }
        }

        public override bool Move(GameSence scence)
        {
            bool isMove = true;
            if (!IsMove)
            {
                return isMove;
            }
            else if (Veloctity.up == this.Velocity.Veloctity && this.Position.X > 1)
            {

                this.Position.X--;
                isMove = true;
            }
            else if (Veloctity.down == this.Velocity.Veloctity && this.Position.X < scence.X - 2)
            {

                this.Position.X++;
                isMove = true;

            }
            else if (Veloctity.left == this.Velocity.Veloctity && this.Position.Y > 1)
            {

                this.Position.Y--;
                isMove = true;

            }
            else if (Veloctity.right == this.Velocity.Veloctity && this.Position.Y < scence.Y - 2)
            {

                this.Position.Y++;
                isMove = true;

            }
            else
            {
                isMove = true;
            }
            return isMove;
        }

       
    }
}
