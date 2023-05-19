
namespace GameLib
{
    public class GenericTween<T> : BlankEntity, Task
    {
        GenericTweenRange<T> range;

        private bool started;
        private float accumulator;


        public delegate void TweenCallbackFunc(T currentValue);
        TweenCallbackFunc func;
        protected void SetCallBack(TweenCallbackFunc func)
        {
            this.func = func;
        }

        public GenericTween(GenericTweenRange<T> range, TweenCallbackFunc func)
        {
            this.range = range;
            this.func = func;

            started = false;
        }
        public override void FrameUpdate(float deltaTime)
        {
            base.FrameUpdate(deltaTime);
            if (IsStop())
                return;

            accumulator += deltaTime;
            T valueAccum;
            if (accumulator >= range.Duration)
            {
                started = false;
                valueAccum = range.EndValue;
            }
            else
                valueAccum = range.GetCurrentValue(accumulator);

            func(valueAccum);
        }
        public bool IsStop()
        {
            return !started;
        }

        public Task Start()
        {
            accumulator = 0.0f;
            started = true;
            return this;
        }
    }
}
