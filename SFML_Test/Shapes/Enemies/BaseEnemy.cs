using SFML.Graphics;
using SFML.System;
using SFML_Test.Extensions;

namespace SFML_Test.Enemies
{
    public class BaseEnemy : BaseEntity
    {
        public int Health { get; set; }

        public BaseEnemy(RenderWindow window, Map map) 
            : base(window, map)
        {
            this.Shape = new CircleShape(15, 8)
            {
                Position = new Vector2f(900, 500),
                FillColor = new Color().FromRgb(255, 204, 255, 255)
            };

            this.Health = 3;
        }

        public override void Draw()
        {
            this.Window.Draw(this.Shape);
        }

        public override bool DetectCollision(Shape shape)
        {
            if (base.DetectCollision(shape))
            {
                this.Health--;
                return true;
            }

            return false;
        }

        public override bool CanBeRemoved()
        {
            return this.Health <= 0;
        }
    }
}