using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace GameLib
{
    public class CircleEntity : CircleShape, Entity
    {
        public CircleEntity(float radius) : base(radius)
        {
        }


        private ParentImpl parentImpl;

        public Entity? Parent { get { return parentImpl.Parent; } }
        public void _NotifyParentAdded(Entity parent)
        {
            parentImpl._NotifyParentAdded(parent);
        }
        public void _NotifyParentRemoved()
        {
            parentImpl._NotifyParentRemoved();
        }
        public void Detach()
        {
            parentImpl.Detach(this);
        }
        public Transform GlobalTransform { get { return parentImpl.CalcGlobalTransform(this.Transform); } }

        public virtual void FrameUpdate(float deltaTime)
        {
        }

        public virtual void KeyPressed(KeyEventArgs e)
        {
        }

        public virtual void KeyReleased(KeyEventArgs e)
        {
        }

        public virtual void MouseButtonPressed(MouseButtonArguments e)
        {
        }

        public virtual void MouseButtonReleased(MouseButtonArguments e)
        {
        }

        public virtual void MouseMoved(MouseMoveArguments e)
        {
        }

        public virtual void MouseWheelScrolled(MouseWheelScrollArguments e)
        {
        }

        public virtual void PhysicsUpdate(float fixTime)
        {
        }

        public virtual void TextEntered(TextEventArgs e)
        {
        }
    }
}
