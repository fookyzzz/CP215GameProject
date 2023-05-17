using GameLib;
using SFML.Graphics;
using SFML.Window;

namespace GameLib
{
    public class EventForwarder : BlankEntity
    {
        protected Entity? forwardToObj;

        public override void Draw(RenderTarget target, RenderStates states)
        {
            forwardToObj?.Draw(target, states);
        }

        public override void FrameUpdate(float deltaTime)
        {
            forwardToObj?.FrameUpdate(deltaTime);
        }

        public override void KeyPressed(KeyEventArgs e)
        {
            forwardToObj?.KeyPressed(e);
        }

        public override void KeyReleased(KeyEventArgs e)
        {
            forwardToObj?.KeyReleased(e);
        }

        public override void MouseButtonPressed(MouseButtonArguments e)
        {
            forwardToObj?.MouseButtonPressed(e);
        }

        public override void MouseButtonReleased(MouseButtonArguments e)
        {
            forwardToObj?.MouseButtonReleased(e);
        }

        public override void MouseMoved(MouseMoveArguments e)
        {
            forwardToObj?.MouseMoved(e);
        }

        public override void MouseWheelScrolled(MouseWheelScrollArguments e)
        {
            forwardToObj?.MouseWheelScrolled(e);
        }

        public override void PhysicsUpdate(float fixTime)
        {
            forwardToObj?.PhysicsUpdate(fixTime);
        }

        public override void TextEntered(TextEventArgs e)
        {
            forwardToObj?.TextEntered(e);
        }
    }
}
