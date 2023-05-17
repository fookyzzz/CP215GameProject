
namespace GameLib
{
    public class Trigger : BlankEntity
    {
        public float Duration { get; set; }
        public bool Continuous { get; set; } = true;
        private float remainder;

        public delegate void VoidFunc();
        private VoidFunc func;

        public Trigger(float duration, bool continuous, VoidFunc func)
        {
            Duration = duration;
            Continuous = continuous;
            this.func = func;
        }

        public override void PhysicsUpdate(float fixTime)
        {
            remainder += fixTime;
            if (remainder >= Duration)
            {
                remainder -= Duration;
                func();
            }
        }
    }
}
