using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace SFML_Test
{
    public class GameLoop
    {
        public readonly RenderWindow Window;
        private readonly Clock _clock;

        public Map Map;

        public GameLoop(RenderWindow window)
        {
            this.Window = window;

            this._clock = new Clock();
            this._clock.Restart();

            this.InitializeMap();
        }

        private void InitializeMap()
        {
            this.Map = new Map(this);
        }

        public void Start()
        {
            while (true)
            {
                if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                {
                    break;
                }

                this.Map.DetectMapMovement();

                this.Window.DispatchEvents();

                this._clock.Restart();

                this.Window.Clear();
                this.Map.Draw();

                this.Window.Display();
            }

            this.Window.Close();

            Console.WriteLine("Thank you for playing this game!");
            Console.ReadKey();
        }


    }
}