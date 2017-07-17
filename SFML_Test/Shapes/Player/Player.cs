using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML_Test.Shapes.Player.Abilities;

namespace SFML_Test.Shapes.Player
{
    public class Player : BaseEntity
    {
        public override Shape Shape { get; set; }

        public float Velocity => 0.05f;

        public Direction CurrentDirection { get; private set; }

        public IList<Lazor> Lazors;

        public bool IsLazorOnCooldown => this._lazorCooldown >= 0;

        private int _lazorCooldown;

        public Player(RenderWindow window, Map map)
            : base(window, map)
        {
            this.Shape = new CircleShape(15, 8) { Position = new Vector2f(500, 500) };

            this.Lazors = new List<Lazor>();
        }

        public override void Draw()
        {
            this.DrawLazors();

            this.Window.Draw(this.Shape);
        }

        private void DrawLazors()
        {
            this._lazorCooldown--;

            if (this.Lazors.Count == 0)
                return;

            for (var lazorI = this.Lazors.Count - 1; lazorI >= 0; lazorI--)
            {
                if (this.Lazors[lazorI].CanBeRemoved())
                {
                    this.Lazors.RemoveAt(lazorI);
                    continue;
                }

                this.Lazors[lazorI].Draw();
            }
        }

        public void Move(Direction direction)
        {
            var newPos = new Vector2f(this.Shape.Position.X, this.Shape.Position.Y);
            var oldPos = this.Shape.Position;

            newPos = this.MoveToDirection(newPos, direction, this.Velocity);

            this.Shape.Position = newPos;

            if (this.Map.CanMove(this.Shape) == false)
            {
                this.Shape.Position = oldPos;
                return;
            }

            this.CurrentDirection = direction;
        }

        private Vector2f MoveToDirection(Vector2f pos, Direction direction, float lenght)
        {
            switch (direction)
            {
                case Direction.Up:
                    pos.Y = pos.Y - lenght;
                    break;
                case Direction.Down:
                    pos.Y = pos.Y + lenght;
                    break;
                case Direction.Left:
                    pos.X = pos.X - lenght;
                    break;
                case Direction.Right:
                    pos.X = pos.X + lenght;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }

            return pos;
        }

        public void FireLazor()
        {
            if (this.IsLazorOnCooldown)
                return;

            this.Lazors.Add(new Lazor(this.Window, this.Map, this, BeamTypes.Cone));
            this._lazorCooldown = (int)TimeSpan.FromSeconds(2).TotalMilliseconds;
        }

        public override bool DetectCollision(Shape shape)
        {
            return false;
        }
    }
}