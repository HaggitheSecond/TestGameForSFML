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
    class Program
    {
        public static RenderWindow Window;

        public static void Main(string[] args)
        {
            Window = new RenderWindow(new VideoMode(1200, 600), "Spiel");
            Window.SetVisible(true);
            Window.Closed += OnClosed;

            var loop = new GameLoop(Window);
            loop.Start();
        }

        private static void OnClosed(object sender, EventArgs e)
        {
            Window.Close();
            Environment.Exit(0);
        }
    }
}


