using SFML.System;

namespace GameLib
{
    public class DelayTask : BlankEntity, Task
    {
        private float duration;
        private bool started;
        private float accumulator;
        private bool finished;

        public DelayTask(float duration)
        {
            this.duration = duration;
            started = false;
            finished = false;
        }

        public void Start()
        {
            accumulator = 0.0f;
            started = true;
        }

        private bool IsStart()
        {
            return started;
        }

        public override void PhysicsUpdate(float fixTime)
        {
            if(!IsStart() || finished) 
                return;

            accumulator += fixTime;
            if (accumulator >= duration)
            {
                finished = true;
            }
        }

        public bool IsFinish()
        {
            return finished;
        }
    }
}
