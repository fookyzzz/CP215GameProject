using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace GameLib
{
    public abstract class GenericButton : BlankTransformableEntity
    {
        //------------------- Public Interface -------------------------------
        public delegate void ButtonClickedDelegate(GenericButton button);
        public event ButtonClickedDelegate ButtonClicked = delegate { };

        //------------------- Protected Interface ----------------------------
        protected Vector2f buttonSize { get; private set; }
        protected enum ButtonState { Pressed, Highlight, Normal };
        protected abstract void UpdateVisual(ButtonState state);
        protected void InvokeClick()
        {
            ButtonClicked.Invoke(this); // ยิง event
        }
        protected GenericButton(Vector2f buttonSize)
        {
            this.buttonSize = buttonSize;
        }

        //------------------- Private Implementation Details -----------------
        private bool bPressed = false; // กดโดนที่ปุ่ม
        private bool bHover = false;   // Mouse อยู่บริเวณปุ่ม
        public override void MouseMoved(MouseMoveArguments e)
        {
            base.MouseMoved(e);
            var worldPoint = e.Point;
            var localPoint = this.GlobalTransform.GetInverse().TransformPoint(worldPoint);
            FloatRect rect = GetButtonRect();

            bHover = rect.Contains(localPoint.X, localPoint.Y);
            UpdateVisual();
        }
        public override void MouseButtonPressed(MouseButtonArguments e)
        {
            base.MouseButtonPressed(e);
            if (e.Button != Mouse.Button.Left)
                return;

            if (bHover)
                bPressed = true;

            UpdateVisual();
        }
        public override void MouseButtonReleased(MouseButtonArguments e)
        {
            base.MouseButtonReleased(e);
            if (e.Button != Mouse.Button.Left)
                return;

            if (bPressed && bHover)
                InvokeClick(); // ยิง event

            bPressed = false;
            UpdateVisual();
        }
        private FloatRect GetButtonRect() // Local Rectangle จุดมุมอยู่ที่ (0,0)
        {
            return new FloatRect(new Vector2f(), buttonSize);
        }
        private void UpdateVisual()
        {
            ButtonState state;
            if (bHover && bPressed)
                state = ButtonState.Pressed;
            else if (bHover && !bPressed || bPressed && !bHover)
                state = ButtonState.Highlight;
            else
                state = ButtonState.Normal;

            UpdateVisual(state);
        }
    }
}
