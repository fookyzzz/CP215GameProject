using SFML.Window;

namespace GameLib
{
    public class FullScreenToggler : BlankEntity
    {
        FullGameWindow window;

        public FullScreenToggler(FullGameWindow window)
        {
            this.window = window;
        }

        public override void KeyPressed(KeyEventArgs e)
        {
            base.KeyPressed(e);
            if (e.Code == Keyboard.Key.F11 && !e.Control && !e.Alt && !e.Shift && !e.System)
                window.ToggleFullScreen();
        }
    }
}
