using ConsoleGame.Component;

namespace ConsoleGame.model
{
    public class Skill : Sprite
    {
        private int damage;

        public int Damage { get => damage; set => damage = value; }

        public Skill(int damage, PositionComponent position, Veloctity veloctity)
        {
            this.Damage = damage;
            this.Position.X = position.X;
            this.Position.Y = position.Y;
            if (Veloctity.up == veloctity)
            {
                this.Position.X -= 1;
                this.Velocity.Veloctity = Veloctity.up;
            }
            else if (Veloctity.down == veloctity)
            {
                this.Position.X += 1;
                this.Velocity.Veloctity = Veloctity.down;
            }
            else if (Veloctity.left == veloctity)
            {
                this.Position.Y -= 1;
                this.Velocity.Veloctity = Veloctity.left;
            }
            else if (Veloctity.right == veloctity)
            {
                this.Position.Y += 1;
                this.Velocity.Veloctity = Veloctity.right;
            }
            this.IsMove = true;

        }

        public override bool Move(GameSence scence)
        {
            bool isMove = false;
            if (Veloctity.up == this.Velocity.Veloctity)
            {

                this.Position.X--;
                isMove = true;
            }
            else if (Veloctity.down == this.Velocity.Veloctity)
            {

                this.Position.X++;
                isMove = true;

            }
            else if (Veloctity.left == this.Velocity.Veloctity)
            {

                this.Position.Y--;
                isMove = true;

            }
            else if (Veloctity.right == this.Velocity.Veloctity)
            {

                this.Position.Y++;
                isMove = true;

            }
            //击中人销毁
            
            //越界销毁
            if (this.Position.X < 1 || this.Position.X > scence.X - 2 || this.Position.Y < 1 || this.Position.Y > scence.Y - 2)
            {
                scence.RemoveSprite(this);
                isMove = false;
            }
            return isMove;

        }
       
    }
}
