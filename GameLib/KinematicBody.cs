using SFML.System;


namespace GameLib
{
    public class KinematicBody : Group
    {
        public Vector2f V = new Vector2f();

        public override void PhysicsUpdate(float fixTime)
        {
            base.PhysicsUpdate(fixTime);
            Position += V * fixTime;
        }
    }
}


