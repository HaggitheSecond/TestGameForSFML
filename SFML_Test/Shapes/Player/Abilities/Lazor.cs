using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace SFML_Test.Shapes.Player.Abilities
{
    public class Lazor : BaseEntity
    {
        private readonly BeamTypes _beamType;

        private IList<RectangleShape> _parts;

        private decimal _lazorTime;

        private readonly decimal _lazorTimeTotal = (int)TimeSpan.FromSeconds(6).TotalMilliseconds;
        private readonly int _partCountTotal = 10;
        private readonly int _beamWidth = 1;

        public Lazor(RenderWindow window, Map map, Player player,BeamTypes beamType)
            : base(window, map)
        {
            this._beamType = beamType;

            this._parts = new List<RectangleShape>();

            var totalShape = this.GetTotalShape(player, map);

            switch (beamType)
            {
                case BeamTypes.Straight:
                    this.MakeStraight(player, map, totalShape);
                    break;
                case BeamTypes.Cone:
                    this.MakeCone(player, map, totalShape);
                    break;
            }

            this._lazorTime = this._lazorTimeTotal;
        }

        private void MakeStraight(Player player, Map map, RectangleShape totalShape)
        {
            totalShape.FillColor = this.GetColor(1);

            this._parts.Add(totalShape);
        }

        private void MakeCone(Player player, Map map, RectangleShape totalShape)
        {

            var partCount = this._partCountTotal;

            // not sure how to make this more comprehensiv
            // not using switch here so due to scope being a bitch there

            if (player.CurrentDirection == Direction.Down)
            {
                var partLenght = Math.Abs(totalShape.Size.Y) / partCount;

                for (int i = 1; i < partCount + 1; i++)
                {
                    var newShape = new RectangleShape()
                    {
                        Position = new Vector2f(totalShape.Position.X - this._beamWidth * (i - 1) / 2, totalShape.Position.Y + (partLenght * (i - 1))),
                        Size = new Vector2f(this._beamWidth * i, partLenght),
                        FillColor = this.GetColor(i)
                    };

                    this._parts.Add(newShape);
                }
            }
            else if (player.CurrentDirection == Direction.Up)
            {
                var partLenght = Math.Abs(totalShape.Size.Y) / partCount;

                for (int i = 1; i < partCount + 1; i++)
                {
                    var newShape = new RectangleShape()
                    {
                        Position = new Vector2f(totalShape.Position.X - this._beamWidth * (i - 1) / 2, totalShape.Position.Y - (partLenght * (i - 1))),
                        Size = new Vector2f(this._beamWidth * i, partLenght * -1),
                        FillColor = this.GetColor(i)
                    };

                    this._parts.Add(newShape);
                }
            }
            else if (player.CurrentDirection == Direction.Left)
            {
                var partLenght = Math.Abs(totalShape.Size.X) / partCount;

                for (int i = 1; i < partCount + 1; i++)
                {
                    var newShape = new RectangleShape()
                    {
                        Position = new Vector2f(totalShape.Position.X - (partLenght * (i - 1)), totalShape.Position.Y - this._beamWidth * (i - 1) / 2),
                        Size = new Vector2f(partLenght * -1, this._beamWidth * i),
                        FillColor = this.GetColor(i)
                    };

                    this._parts.Add(newShape);
                }
            }
            else if (player.CurrentDirection == Direction.Right)
            {
                var partLenght = Math.Abs(totalShape.Size.X) / partCount;

                for (int i = 1; i < partCount + 1; i++)
                {
                    var newShape = new RectangleShape()
                    {
                        Position = new Vector2f(totalShape.Position.X + (partLenght * (i - 1)), totalShape.Position.Y - this._beamWidth * (i - 1) / 2),
                        Size = new Vector2f(partLenght, this._beamWidth * i),
                        FillColor = this.GetColor(i)
                    };

                    this._parts.Add(newShape);
                }
            }
        }

        private Color GetColor(int i)
        {
            var color = Color.Red;

            color.R = (byte)(byte.MaxValue - (byte)i * 10);
            return color;
        }

        private RectangleShape GetTotalShape(Player player, Map map)
        {
            var circleShape = player.Shape as CircleShape;
            if (circleShape == null)
                throw new Exception("You done goofed");

            var bounds = circleShape.GetGlobalBounds();

            var shape = new RectangleShape
            {
                Position = new Vector2f(bounds.Left + bounds.Width / 2 - 5, bounds.Top + bounds.Height / 2 - 5)
            };

            for (var i = 0; i < 2000; i++)
            {
                switch (player.CurrentDirection)
                {
                    case Direction.Up:
                        shape.Size = new Vector2f(10, i * -1);
                        break;
                    case Direction.Down:
                        shape.Size = new Vector2f(10, i);
                        break;
                    case Direction.Left:
                        shape.Size = new Vector2f(i * -1, 10);
                        break;
                    case Direction.Right:
                        shape.Size = new Vector2f(i, 10);
                        break;
                }

                if (map.CanMove(shape) == false)
                {
                    break;
                }
            }

            return shape;
        }

        public override bool CanBeRemoved()
        {
            return this._lazorTime <= 0;
        }

        public override void Draw()
        {
            if (this._partCountTotal + 2000 <= this._parts.Count)
            {
                var currentPartCount = Math.Ceiling(this._lazorTime / (this._lazorTimeTotal / this._partCountTotal));
                var currentPartCountFromZero = this._partCountTotal - currentPartCount;
                var temp = (int)currentPartCountFromZero;

                this._parts.RemoveAt(temp);
            }

            foreach (var currentPart in this._parts)
            {
                this.Window.Draw(currentPart);
            }

            this._lazorTime--;
        }
    }
}