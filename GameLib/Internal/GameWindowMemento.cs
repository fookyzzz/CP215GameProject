using SFML.Graphics;
using SFML.System;

namespace GameLib.Internal
{
    internal class GameWindowMemento
    {
        internal float PixelPerUnit;
        internal Vector2f FixWorldSize;
        internal Color BackgroundColor;
        internal Color UnusedSpaceColor;
        internal CollisionDetection CollisionDetectionUnit;
        internal bool KeyRepeatEnabled;
        internal uint FramerateLimit;
    }
}
