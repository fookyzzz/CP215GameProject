using SFML.System;
using SFML.Window;

namespace GameLib
{
    public struct MouseMoveArguments
    {
        public Vector2f Point;
    }
    public struct MouseButtonArguments
    {
        public Vector2f Point;
        public Mouse.Button Button;
    }
    public struct MouseWheelScrollArguments
    {
        public Vector2f Point;
        public Mouse.Wheel Wheel;
        public float Delta;
    }
}
