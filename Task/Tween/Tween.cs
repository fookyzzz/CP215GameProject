
namespace GameLib
{
    public class Tween : GenericTween<float>
    {
        public Tween(float startValue, float endValue, float duration, TweenCallbackFunc func)
            : base(new Range(startValue, endValue, duration), func)
        {
        }
        protected Tween(float startValue, float endValue, float duration)
            : this(startValue, endValue, duration, null)
        {
        }
        private class Range : GenericTweenRange<float>
        {
            float startValue;
            float endValue;
            float duration;
            float speed;

            public Range(float startValue, float endValue, float duration)
            {
                this.startValue = startValue;
                this.endValue = endValue;
                this.duration = duration;
                this.speed = (endValue - startValue) / duration;
            }

            public float GetCurrentValue(float accumulatedTime)
            {
                return startValue + speed * accumulatedTime;
            }

            public float EndValue { get => endValue; }
            public float Duration { get => duration; }
        }
    }
}
