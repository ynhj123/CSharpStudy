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
            this.Id = System.Guid.NewGuid().ToString("N");

        }
        int attchInterval = 5;

        public int AttchInterval { get => attchInterval; set => attchInterval = value; }

        public void attach(GameSence scence)
        {
            Skill skill = new Skill(10, this.Position, this.Velocity.Veloctity);
            skill.Style = '*';
            scence.AddSprite(skill);

        }

      

        public override bool Move(GameSence scence)
        {

            MsgMove msgMove = new MsgMove();
            if (!IsMove)
            {
                return IsMove;
            }
            else if (Veloctity.up == this.Velocity.Veloctity && this.Position.X > 1)
            {
                this.Position.X--;
            }
            else if (Veloctity.down == this.Velocity.Veloctity && this.Position.X < scence.X - 2)
            {

                this.Position.X++;

            }
            else if (Veloctity.left == this.Velocity.Veloctity && this.Position.Y > 1)
            {
                this.Position.Y--;
            }
            else if (Veloctity.right == this.Velocity.Veloctity && this.Position.Y < scence.Y - 2)
            {
                this.Position.Y++;
            }
            msgMove.spriteId = this.Id;
            msgMove.x = this.Position.X;
            msgMove.y = this.Position.Y;
            msgMove.veloctity = (int)this.Velocity.Veloctity;
            NetManagerEvent.Send(msgMove);
            IsMove = false;
            return IsMove;
        }


    }
}
