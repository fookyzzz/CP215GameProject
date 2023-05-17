using SFML.Graphics;
using SFML.System;
using System;

namespace GameLib
{
    public static class RandomUtil
    {
        private static Random random = new Random();
        public static int Next(int maxValue)
        {
            return random.Next(maxValue);
        }

        public static int Next(int minValue, int maxValue)
        {
            return random.Next(minValue, maxValue);
        }
        public static float NextSingle()
        {
            return random.NextSingle();
        }

        public static Vector2f Direction()
        {
            float theta = RandomUtil.NextSingle() * 2 * MathF.PI;
            return new Vector2f(MathF.Cos(theta), MathF.Sin(theta));
        }

        public static Color Color()
        {
            return new Color((byte)Next(256), (byte)Next(256), (byte)Next(256));
        }
    }
}
