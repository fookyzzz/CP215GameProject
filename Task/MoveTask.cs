using SFML.System;

namespace GameLib
{
    public class MoveTask : BlankEntity, Task
    {
        private KinematicBody body;
        private Vector2f v;
        private float duration;
        private bool started;
        private float accumulator;

        public MoveTask(KinematicBody body, Vector2f v, float duration)
        {
            this.body = body;
            this.v = v;  // Bug: ปัญหาคือ เมื่อสร้าง mover จะ set V เลย ทั้งที่จริงๆ ยังไม่ได้เริ่มทำงาน
            this.duration = duration;
            started = false;
        }

        public Task Start()
        {
            body.V = v;
            accumulator = 0.0f;
            started = true;
            return this;
        }

        public override void PhysicsUpdate(float fixTime)
        {
            if(IsStop()) 
                return;

            accumulator += fixTime;
            if (accumulator >= duration)
            {
                body.V = new Vector2f();
                started = false;
            }
        }

        public bool IsStop()
        {
            return !started;
        }
    }
}
