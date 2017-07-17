using SFML.Graphics;

namespace SFML_Test.Shapes.MapParts
{
    public class Wall : BaseEntity
    {
        public Wall(RenderWindow window, Map map, RectangleShape shape)
            : base(window, map)
        {
            this.Shape = shape;
        }

        public override void Draw()
        {
            this.Window.Draw(this.Shape);
        }
    }
}