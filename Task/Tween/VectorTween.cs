
using SFML.Graphics;
using SFML.System;

namespace GameLib
{
    public class VectorTween : GenericTween<Vector2f>
    {
        public VectorTween(Vector2f startValue, Vector2f endValue, float duration, TweenCallbackFunc func)
            : base(new Range(startValue, endValue, duration), func)
        {
        }

        private class Range : GenericTweenRange<Vector2f>
        {
            Vector2f startValue;
            Vector2f endValue;
            float duration;
            Vector2f speed;

            public Range(Vector2f startValue, Vector2f endValue, float duration)
            {
                this.startValue = startValue;
                this.endValue = endValue;
                this.duration = duration;
                this.speed = (endValue - startValue) / duration;
            }

            public Vector2f GetCurrentValue(float accumulatedTime)
            {
                return startValue + speed * accumulatedTime;
            }

            public Vector2f EndValue { get => endValue; }
            public float Duration { get => duration; }
        }
    }
}
