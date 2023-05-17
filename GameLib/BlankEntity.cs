using SFML.Graphics;
using SFML.Window;
using System.Diagnostics;

namespace GameLib
{
    public class BlankEntity : Entity
    {
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

        public Transform GlobalTransform { get { return parentImpl.CalcGlobalTransform(Transform.Identity); } }

        public virtual void Draw(RenderTarget target, RenderStates states)
        {
        }

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

