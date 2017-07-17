using SFML.Graphics;

namespace SFML_Test.Extensions
{
    public static class ColorExtension
    {
        public static Color FromRgb(this Color self, byte r, byte g, byte b, byte a)
        {
            return new Color
            {
                R = r,
                G = g,
                B = b,
                A = a
            };
        }
    }
}