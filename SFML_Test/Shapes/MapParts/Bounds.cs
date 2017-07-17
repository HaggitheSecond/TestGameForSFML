using SFML.Graphics;
using SFML.System;

namespace SFML_Test.Shapes.MapParts
{
    public class Bounds : BaseEntity
    {
        private RectangleShape _upperBound;
        private RectangleShape _lowerBound;
        private RectangleShape _rightBound;
        private RectangleShape _leftBound;

        private int _boundWidth;
        private Color _color;

        public Bounds(RenderWindow window, Map map)
            : base(window, map)
        {
            this._boundWidth = 10;
            this._color = Color.Green;

            this._upperBound = new RectangleShape(new Vector2f(this.Map.Width, this._boundWidth))
            {
                Position = new Vector2f(0, 0),
                FillColor = this._color
            };
            this._lowerBound = new RectangleShape(new Vector2f(this.Map.Width, this._boundWidth))
            {
                Position = new Vector2f(0, this.Map.Height - this._boundWidth),
                FillColor = this._color
            };
            this._rightBound = new RectangleShape(new Vector2f(this._boundWidth, this.Map.Height))
            {
                Position = new Vector2f(0, 0),
                FillColor = this._color
            };
            this._leftBound = new RectangleShape(new Vector2f(this._boundWidth, this.Map.Height))
            {
                Position = new Vector2f(this.Map.Width - this._boundWidth, 0),
                FillColor = this._color
            };
        }

        public override void Draw()
        {
            base.Window.Draw(this._upperBound);
            base.Window.Draw(this._lowerBound);
            base.Window.Draw(this._rightBound);
            base.Window.Draw(this._leftBound);
        }

        public override bool DetectCollision(Shape shape)
        {
            var shapeBounds = shape.GetGlobalBounds();

            return shapeBounds.Intersects(this._upperBound.GetGlobalBounds())
                   || shapeBounds.Intersects(this._lowerBound.GetGlobalBounds())
                   || shapeBounds.Intersects(this._rightBound.GetGlobalBounds())
                   || shapeBounds.Intersects(this._leftBound.GetGlobalBounds());
        }
    }
}