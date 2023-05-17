using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace GameLib
{
    public static class MouseEventExtension
    {
        public static Vector2i GetPixel(this MouseButtonEventArgs e)
        {
            return new Vector2i(e.X, e.Y);
        }
        public static Vector2i GetPixel(this MouseMoveEventArgs e)
        {
            return new Vector2i(e.X, e.Y);
        }
        public static Vector2i GetPixel(this MouseWheelScrollEventArgs e)
        {
            return new Vector2i(e.X, e.Y);
        }
    }
}
