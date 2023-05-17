using SFML.Graphics;
using SFML.Window;
using System.Diagnostics;

namespace GameLib
{
    public interface Entity : Drawable
    {
        // Updates (Process)
        void FrameUpdate(float deltaTime);
        void PhysicsUpdate(float fixTime);

        // Events (Input)
        void KeyPressed(KeyEventArgs e);
        void TextEntered(TextEventArgs e);
        void KeyReleased(KeyEventArgs e);
        void MouseButtonPressed(MouseButtonArguments e);
        void MouseButtonReleased(MouseButtonArguments e);
        void MouseMoved(MouseMoveArguments e);
        void MouseWheelScrolled(MouseWheelScrollArguments e);

        //--------- Parent Logics ------------
        Entity? Parent { get; }
        void Detach();
        void _NotifyParentAdded(Entity parent);
        void _NotifyParentRemoved();
        Transform GlobalTransform { get; } // ถ้าเรียกโดยไม่มี Parent จะ Error; ยกเว้นตนเองคือ Group
    }
}


