using SFML.System;
using SFML.Window;
using System;

namespace GameLib
{
    public class DirectionKey
    {
        private static float sin45 = MathF.Sin(MathF.PI / 4.0f);
        public static Vector2f Direction
        {
            get
            {
                Vector2f direction = new Vector2f();
                if (Keyboard.IsKeyPressed(Keyboard.Key.D))
                    direction += new Vector2f(1, 0);
                if (Keyboard.IsKeyPressed(Keyboard.Key.A))
                    direction += new Vector2f(-1, 0);
                if (Keyboard.IsKeyPressed(Keyboard.Key.W))
                    direction += new Vector2f(0, -1);
                if (Keyboard.IsKeyPressed(Keyboard.Key.S))
                    direction += new Vector2f(0, 1);
                return direction;
            }
        }
        public static Vector2f Direction4(Keyboard.Key key)
        {
            Vector2f direction = new Vector2f();
            if (key == Keyboard.Key.D)
                direction += new Vector2f(1, 0);
            if (key == Keyboard.Key.A)
                direction += new Vector2f(-1, 0);
            if (key == Keyboard.Key.W)
                direction += new Vector2f(0, -1);
            if (key == Keyboard.Key.S)
                direction += new Vector2f(0, 1);
            return direction;
        }
        public static Vector2f Normalized
        {
            get
            {
                var direction = Direction;
                if (direction.X != 0 && direction.Y != 0)
                    direction *= sin45;
                return direction;
            }
        }

        // ถ้ามีปุ่มเฉียง จะตัดทิ้งกลายเป็น (0,0)
        public static Vector2f Normalized4
        {
            get
            {
                var direction = Direction;
                if (direction.X != 0 && direction.Y != 0)
                    direction = new Vector2f();
                return direction;
            }
        }
    }
}
