using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML;
using SFML.Window;
using SFML.Graphics;
using SFML.System;
using SFML.Audio;

namespace SFML_Test
{
    static class Constants
    {
        public static float ballRadius = 10.0f;
        public static float ballVelocity = 300.0f;
        public static uint windowWidth = 800;
        public static uint windowHeight = 600;
    }
    public class Ball
    {

        public CircleShape shape = new CircleShape();
        Vector2f velocity = new Vector2f(-Constants.ballVelocity, -Constants.ballVelocity);

        public Ball(float mX, float mY)
        {
            shape.Position = new Vector2f(mX, mY);
            shape.Radius = Constants.ballRadius;
            shape.FillColor = Color.Red;
            shape.Origin = new Vector2f(Constants.ballRadius, Constants.ballRadius);
        }

        public void update(Time t)
        {

            shape.Position += new Vector2f(velocity.X * t.AsSeconds(), velocity.Y * t.AsSeconds());

            if (shape.Position.X - shape.Radius < 0)
                velocity.X = Constants.ballVelocity;
            else if (shape.Position.X + shape.Radius > Constants.windowWidth)
                velocity.X = -Constants.ballVelocity;

            if (shape.Position.Y - shape.Radius < 0)
                velocity.Y = Constants.ballVelocity;
            else if (shape.Position.Y + shape.Radius > Constants.windowHeight)
                velocity.Y = -Constants.ballVelocity;
        }
    }

    class Program
    {
        private static RenderWindow window;

        public static void Main(string[] args)
        {
            bool running = true;
            window = new RenderWindow(new VideoMode(Constants.windowWidth, Constants.windowHeight), "Spiel");
            window.SetVisible(true);
            window.Closed += new EventHandler(OnClosed);

            Ball ball = new Ball(600, 500);
            Clock clock = new Clock();
            clock.Restart();

            while (running && window.IsOpen)
            {
                // Keyboardtasten auslesen
                if (Keyboard.IsKeyPressed(Keyboard.Key.Q))
                {
                    running = false;
                }
                window.DispatchEvents();
                window.Clear(Color.Black);

                // Logik
                ball.update(clock.ElapsedTime);

                // Zeichnen
                clock.Restart();
                //window.Clear(Color.Black);
                window.Draw(ball.shape);

                window.Display();
            }
        }

        private static void OnClosed(object sender, EventArgs e)
        {
            window.Close();
        }
    }
}


