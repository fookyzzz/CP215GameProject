using GameLib.Internal;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace GameLib
{
    public class FullGameWindow
    {
        public float PixelPerUnit { get => Window.PixelPerUnit; set => Window.PixelPerUnit = value; }
        public Vector2f Center { get => Window.Center; set => Window.Center = value; }
        public Color BackgroundColor { get => Window.BackgroundColor; set => Window.BackgroundColor = value; }
        public Color UnusedSpaceColor { get => Window.UnusedSpaceColor; set => Window.UnusedSpaceColor = value; }

        VideoMode videoMode;
        string title;
        bool antialias;
        bool isFullScreen = false;
        internal Vector2i position;
        GameWindowMemento memento;

        private GameWindow Window { get; set; }

        public FullGameWindow(VideoMode videoMode, string title)
            : this(videoMode, title, GameWindow.DefaultAntialias)
        {
        }

        public FullGameWindow(VideoMode videoMode, string title, bool antialias)
            : this(videoMode, title, Styles.Default, antialias)
        {
        }
        public FullGameWindow(VideoMode videoMode, string title, Styles style, bool antialias)
        {
            this.videoMode = videoMode;
            this.title = title;
            this.antialias = GameWindow.DefaultAntialias;
            this.Window = new GameWindow(videoMode, title, antialias);
        }
        public void FixWorldSize(Vector2f fixSize)
        {
            Window.FixWorldSize(fixSize);
        }
        public void SetKeyRepeatEnabled(bool enable)
        {
            Window.SetKeyRepeatEnabled(enable);
        }
        public void SetFramerateLimit(uint limit)
        {
            Window.SetFramerateLimit(limit);
        }

        private bool IsFullScreen(Styles styles)
        {
            return (styles & Styles.Fullscreen) != 0;
        }


        bool toggleWindow = false;
        private GameWindow newWindow;
        public void RunGameLoop(Group allObjs)
        {
            while (true)
            {
                Window.RunGameLoop(allObjs);
                if (!toggleWindow)
                    break;
                Window = newWindow;
                toggleWindow = false;
            }
        }

        public bool IsFullScreen() { return isFullScreen; }
        public void ToggleFullScreen()
        {
            toggleWindow = true;
            if (!isFullScreen)
            {
                var size = Window.Size;
                videoMode = new VideoMode(size.X, size.Y, videoMode.BitsPerPixel);
                position = Window.Position;
            }
            memento = Window.GetMemento();

            Window.Close();

            if (!isFullScreen)
                newWindow = new GameWindow(VideoMode.DesktopMode, title, Styles.Fullscreen, antialias);
            else
            {
                newWindow = new GameWindow(videoMode, title);
                newWindow.Position = position;
            }
            newWindow.SetMemento(memento);

            isFullScreen = !isFullScreen;
        }
    }
}
