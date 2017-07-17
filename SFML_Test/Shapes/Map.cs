using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML_Test.Enemies;
using SFML_Test.Shapes.MapParts;
using SFML_Test.Shapes.Player;

namespace SFML_Test
{
    public class Map
    {
        private readonly GameLoop _gameLoop;

        public uint Width { get; }
        public uint Height { get; }

        public Map(GameLoop gameLoop)
        {
            this._gameLoop = gameLoop;

            this.Width = this._gameLoop.Window.Size.X;
            this.Height = this._gameLoop.Window.Size.Y;

            this._entities = new List<BaseEntity>();

            this._bounds = new Bounds(this._gameLoop.Window, this);
            this._entities.Add(this._bounds);

            this.AddWalls();
            
            this._entities.Add(new BaseEnemy(this._gameLoop.Window, this));

            this._player = new Player(this._gameLoop.Window, this);
            this._entities.Add(this._player);

        }

        private void AddWalls()
        {
            this._entities.Add(new Wall(this._gameLoop.Window, this, new RectangleShape()
            {
                Size = new Vector2f(10, 200),
                Position = new Vector2f(100, 100)
            }));

            this._entities.Add(new Wall(this._gameLoop.Window, this, new RectangleShape()
            {
                Size = new Vector2f(10, 200),
                Position = new Vector2f(300, 100)
            }));

            this._entities.Add(new Wall(this._gameLoop.Window, this, new RectangleShape()
            {
                Size = new Vector2f(200, 10),
                Position = new Vector2f(100, 100)
            }));

            this._entities.Add(new Wall(this._gameLoop.Window, this, new RectangleShape()
            {
                Size = new Vector2f(100, 400),
                Position = new Vector2f(1000, 100)
            }));

            this._entities.Add(new Wall(this._gameLoop.Window, this, new RectangleShape()
            {
                Size = new Vector2f(200, 10),
                Position = new Vector2f(600, 100),
                Rotation = 45f
            }));
        }

        public void DetectMapMovement()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.W) || Keyboard.IsKeyPressed(Keyboard.Key.Up))
            {
                this._player.Move(Direction.Up);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.S) || Keyboard.IsKeyPressed(Keyboard.Key.Down))
            {
                this._player.Move(Direction.Down);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.A) || Keyboard.IsKeyPressed(Keyboard.Key.Left))
            {
                this._player.Move(Direction.Left);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.D) || Keyboard.IsKeyPressed(Keyboard.Key.Right))
            {
                this._player.Move(Direction.Right);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
            {
                this._player.FireLazor();
            }
        }

        public void Draw()
        {
            for (int i = 0; i < this._entities.Count; i++)
            {
                var current = this._entities[i];

                if (current.CanBeRemoved())
                {
                    this._entities.Remove(current);
                    continue;
                }

                current.Draw();
                
            }
        }

        public bool CanMove(Shape shape)
        {
            foreach (var currentEntity in this._entities)
            {
                if (currentEntity.DetectCollision(shape))
                    return false;
            }

            return true;
        }

        private Bounds _bounds;
        private Player _player;

        private IList<BaseEntity> _entities;
    }
}