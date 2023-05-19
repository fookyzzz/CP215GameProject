
using SFML.Graphics;
using SFML.System;

namespace GameLib
{
    public class ColorTween : GenericTween<Color>
    {
        public ColorTween(Color startValue, Color endValue, float duration, TweenCallbackFunc func)
            : base(new Range(startValue, endValue, duration), func)
        {
        }

        private class Range : GenericTweenRange<Color>
        {
            Color startValue;
            Color endValue;
            float duration;
            Vector3f speed;
            float opaqueSpeed;

            public Range(Color startValue, Color endValue, float duration)
            {
                this.startValue = startValue;
                this.endValue = endValue;
                this.duration = duration;
                this.speed = new Vector3f(
                        (endValue.R - startValue.R) / duration,
                        (endValue.G - startValue.G) / duration,
                        (endValue.B - startValue.B) / duration);
                this.opaqueSpeed = (endValue.A - startValue.A) / duration;
            }

            public Color GetCurrentValue(float accumulatedTime)
            {
                return new Color(
                    (byte)(startValue.R + speed.X * accumulatedTime),
                    (byte)(startValue.G + speed.Y * accumulatedTime),
                    (byte)(startValue.B + speed.Z * accumulatedTime),
                    (byte)(startValue.A + opaqueSpeed * accumulatedTime)
                    );
            }

            public Color EndValue { get => endValue; }
            public float Duration { get => duration; }
        }
    }
}
