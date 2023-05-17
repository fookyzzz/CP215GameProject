using System;
using SFML.System;

namespace GameLib
{
    public static class VectorExtension
    {
        public static float Dot(this Vector2f a, Vector2f b)
        {
            return a.X * b.X + a.Y * b.Y;
        }

        public static float Size(this Vector2f a)
        {
            return MathF.Sqrt(a.X * a.X + a.Y * a.Y);
        }

        public static Vector2f Normalize(this Vector2f a)
        {
            float size = a.Size();
            if (size == 0)
                return new Vector2f(0, 0);

            return a / size;
        }
    }
}

