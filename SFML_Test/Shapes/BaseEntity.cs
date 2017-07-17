using SFML.Graphics;

namespace SFML_Test
{
    public abstract class BaseEntity
    {
        public virtual Shape Shape { get; set; }

        protected readonly RenderWindow Window;
        protected readonly Map Map;

        protected BaseEntity(RenderWindow window, Map map)
        {
            this.Window = window;
            this.Map = map;
        }

        public abstract void Draw();

        public virtual bool DetectCollision(Shape shape)
        {
            return this.Shape.GetGlobalBounds().Intersects(shape.GetGlobalBounds());
        }

        public virtual bool CanBeRemoved()
        {
            return false;
        }
    }
}