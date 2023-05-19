using SFML.System;

namespace GameLib
{
    public class DelayTask : BlankEntity, Task
    {
        private float duration;
        private float accumulator;
        private bool stopped;

        public DelayTask(float duration)
        {
            this.duration = duration;
            stopped = true;
        }

        public Task Start()
        {
            accumulator = 0.0f;
            stopped = false;
            return this;
        }

        public override void PhysicsUpdate(float fixTime)
        {
            if(IsStop()) 
                return;

            accumulator += fixTime;
            if (accumulator >= duration)
                stopped = true;
        }

        public bool IsStop()
        {
            return stopped;
        }
    }
}
