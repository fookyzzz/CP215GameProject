using GameLib.Internal;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace GameLib
{
    public class GameWindow : RenderWindow
    {
        private bool keyRepeatEnabled = true;
        private uint framerateLimit = 0;
        private float _pixelPerUnit = 1;
        public float PixelPerUnit { get { return _pixelPerUnit; } set { _pixelPerUnit = value; AdjustView(); } }
        private Vector2f fixWorldSize = new Vector2f(0, 0);

        private Vector2f _center = new Vector2f(); // ในรูปของ world (game) coordinate
        public Vector2f Center
        {
            get { return _center; }
            set
            {
                _center = value;
                var view = this.GetView();
                view.Center = _center;
                this.SetView(view);
            }
        }

        // ปรับ center ให้ด้วย โดยรักษาจุดมุมบนซ้ายให้อยู่ที่เดิม
        public void FixWorldSize(Vector2f fixSize)
        {
            var lefttop = WorldRect().GetPosition();
            this.fixWorldSize = fixSize;
            _center = lefttop + fixWorldSize / 2.0f;
            AdjustView();
        }

        private void OnResized(object? sender, SizeEventArgs size)
        {
            AdjustView();
        }
        private void AdjustView()
        {
            Vector2u screenSize = this.Size;
            View view;
            if (fixWorldSize.X == 0) // ไม่ใช่ mode fix world size
            {
                var lefttop = WorldRect().GetPosition();
                view = new View(new FloatRect(lefttop.X, lefttop.Y, screenSize.X / PixelPerUnit, screenSize.Y / PixelPerUnit));
                view.Viewport = new FloatRect(0, 0, 1f, 1f);
                _center = view.Center;
            }
            else // mode: fix world size
            {
                view = new View(_center, fixWorldSize);
                view.Viewport = CalcFixedViewport(screenSize, fixWorldSize);
            }
            this.SetView(view);
        }

        private FloatRect WorldRect()
        {
            return this.GetView().GetRect();
        }

        private static FloatRect CalcFixedViewport(Vector2u screenSize, Vector2f fixWorldSize)
        {
            float scrRatio = (float)screenSize.X / screenSize.Y;
            float worldRatio = fixWorldSize.X / fixWorldSize.Y;
            if (scrRatio > worldRatio)
            {
                var widthRatio = worldRatio / scrRatio;
                return new FloatRect((1 - widthRatio) / 2, 0, widthRatio, 1);
            }
            else
            {
                var heightRatio = scrRatio / worldRatio;
                return new FloatRect(0, (1 - heightRatio) / 2, 1, heightRatio);
            }
        }
        //----------------------------------------
        public CollisionDetection CollisionDetectionUnit { get; private set; } = new CollisionDetection();
        private Group allObjs = new Group();
        public static bool DefaultAntialias = false;
        public GameWindow(VideoMode mode, string title) : this(mode, title, Styles.Default, DefaultAntialias)
        {
        }
        public GameWindow(VideoMode mode, string title, bool antialias) : this(mode, title, Styles.Default, antialias)
        {

        }
        public GameWindow(VideoMode mode, string title, Styles style, bool antialias) : 
            base(mode, title, style, CreateContext(antialias))
        {
            this.Closed += WindowClosed;

            this.KeyPressed += GameWindow_KeyPressed;
            this.TextEntered += GameWindow_TextEntered;
            this.KeyReleased += GameWindow_KeyReleased;

            this.MouseButtonPressed += GameWindow_MouseButtonPressed;
            this.MouseButtonReleased += GameWindow_MouseButtonReleased;
            this.MouseMoved += GameWindow_MouseMoved;
            this.MouseWheelScrolled += GameWindow_MouseWheelScrolled;
            this.Resized += OnResized;
            AdjustView();
        }
        private static ContextSettings CreateContext(bool antialias)
        {
            var context = new ContextSettings();
            if(antialias)
                context.AntialiasingLevel = 8;
            return context;
        }
        public override void SetKeyRepeatEnabled(bool enable)
        {
            keyRepeatEnabled = enable;
            base.SetKeyRepeatEnabled(enable);
        }

        public override void SetFramerateLimit(uint limit)
        {
            framerateLimit = limit;
            base.SetFramerateLimit(limit);
        }


        private void GameWindow_MouseMoved(object? sender, MouseMoveEventArgs e)
        {
            var args = new MouseMoveArguments() { Point = MapPixelToCoords(e.GetPixel()) };
            allObjs.MouseMoved(args);
        }

        private void GameWindow_MouseButtonReleased(object? sender, MouseButtonEventArgs e)
        {
            var args = new MouseButtonArguments()
            {
                Point = MapPixelToCoords(e.GetPixel()),
                Button = e.Button
            };
            allObjs.MouseButtonReleased(args);
        }

        private void GameWindow_MouseButtonPressed(object? sender, MouseButtonEventArgs e)
        {
            var args = new MouseButtonArguments()
            {
                Point = MapPixelToCoords(e.GetPixel()),
                Button = e.Button
            };
            allObjs.MouseButtonPressed(args);
        }

        private void GameWindow_MouseWheelScrolled(object? sender, MouseWheelScrollEventArgs e)
        {
            var args = new MouseWheelScrollArguments()
            {
                Point = MapPixelToCoords(e.GetPixel()),
                Wheel = e.Wheel,
                Delta = e.Delta
            };
            allObjs.MouseWheelScrolled(args);
        }

        private void GameWindow_KeyReleased(object? sender, KeyEventArgs e)
        {
            allObjs.KeyReleased(e);
        }

        private void GameWindow_TextEntered(object? sender, TextEventArgs e)
        {
            allObjs.TextEntered(e);
        }

        private void GameWindow_KeyPressed(object? sender, KeyEventArgs e)
        {
            allObjs.KeyPressed(e);
        }

        const int cutoffRatio = 20; // physics update will do at most 20 times per frame
        public void RunGameLoop(Group allObjs)
        {
            this.allObjs = allObjs;
            var clock = new Clock();
            float physicsInterval = 1 / 120.0f;
            float physicsCutoff = (-1 / 8f) * physicsInterval;
            float remainder = 0;
            while (this.IsOpen) // Game Loop
            {
                // Event Dispatching
                DispatchEvents();

                // Game State Updating
                float deltaTime = clock.Restart().AsSeconds();
                remainder += deltaTime;
                int i = 0;
                while (remainder - physicsInterval >= physicsCutoff)
                {
                    PhysicsUpdateAll(physicsInterval);
                    CollisionDetectionUnit.DetectAndResolve(allObjs);

                    remainder -= physicsInterval;

                    i++;
                    if (i >= cutoffRatio)
                    {
                        remainder = 0;
                        break;
                    }
                }
                FrameUpdateAll(deltaTime);

                // Rendering
                Clear(UnusedSpaceColor);
                DrawBackground();
                DrawAll();
                Display();
            }
        }

        private void DrawBackground()
        {
            var world = WorldRect();
            var rect = new RectangleShape();
            rect.Size = world.GetSize();
            rect.Position = world.GetPosition();
            rect.FillColor = BackgroundColor;
            this.Draw(rect);
        }

        public Color UnusedSpaceColor { get; set; } = Color.Black;
        public Color BackgroundColor { get; set; } = new Color(200, 200, 200);

        private void PhysicsUpdateAll(float physicsInterval)
        {
            allObjs.PhysicsUpdate(physicsInterval);
        }

        private void FrameUpdateAll(float deltaTime)
        {
            allObjs.FrameUpdate(deltaTime);
        }
        private void DrawAll()
        {
            this.Draw(allObjs);
        }

        void WindowClosed(object? sender, EventArgs e)
        {
            var window = (RenderWindow)sender!;
            window.Close();
        }

        internal GameWindowMemento GetMemento()
        {
            var m = new GameWindowMemento()
            {
                KeyRepeatEnabled = this.keyRepeatEnabled,
                FramerateLimit = this.framerateLimit,
                BackgroundColor = this.BackgroundColor,
                UnusedSpaceColor = this.UnusedSpaceColor,
                PixelPerUnit = this.PixelPerUnit,
                FixWorldSize = this.fixWorldSize,
                CollisionDetectionUnit = this.CollisionDetectionUnit,
            };
            return m;
        }

        internal void SetMemento(GameWindowMemento m)
        {
            SetKeyRepeatEnabled(m.KeyRepeatEnabled);
            SetFramerateLimit(m.FramerateLimit);
            BackgroundColor = m.BackgroundColor;
            UnusedSpaceColor = m.UnusedSpaceColor;
            PixelPerUnit = m.PixelPerUnit;
            FixWorldSize(m.FixWorldSize);
            CollisionDetectionUnit = m.CollisionDetectionUnit;
        }
    }
}
