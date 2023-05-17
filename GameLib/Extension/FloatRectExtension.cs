using SFML.Graphics;
using SFML.System;

namespace GameLib
{
    public static class FloatRectExtension
    {
        public static bool Contains(this FloatRect rect, Vector2f point) { return rect.Contains(point.X, point.Y); }
        public static Vector2f GetSize(this FloatRect rect)
        {
            return new Vector2f(rect.Width, rect.Height);
        }
        public static Vector2f GetPosition(this FloatRect rect)
        {
            return new Vector2f(rect.Left, rect.Top);
        }

        public static Vector2f GetCenter(this FloatRect rect)
        {
            return new Vector2f(rect.Left + rect.Width / 2, rect.Top + rect.Height / 2);
        }

        // return up (0,-1), down (0,1), left(-1,0), right(1,0) or none (0,0)
        public static Vector2f RelativeDirection(this FloatRect a, FloatRect overlap)
        {
            //if(overlap.hittest(a.center))
            //    return 0,0;
            var point = (overlap.GetCenter() - a.GetCenter());
            if (point.Y * a.Width > a.Height * point.X) // down or left
                if (point.Y * a.Width > -a.Height * point.X) // down or right
                    return new Vector2f(0, 1);
                else
                    return new Vector2f(-1, 0);
            else
                if (point.Y * a.Width > -a.Height * point.X) // down or right
                    return new Vector2f(1, 0);
                else
                    return new Vector2f(0, -1);

        }

        public static FloatRect AdjustSize(this FloatRect rect, float widthRatio, float heightRatio)
        {
            rect.Left += rect.Width * (1 - widthRatio) / 2;
            rect.Width *= widthRatio;

            rect.Top += rect.Height * (1 - heightRatio) / 2;
            rect.Height *= heightRatio;

            return rect;
        }

    }
}
