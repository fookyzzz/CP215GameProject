using SFML.Graphics;
using SFML.System;
using System;

namespace GameLib
{
    public class FPS : BlankEntity
    {
        float currentTime;
        int frameTick;
        int physicsTick;
        private Text text;
        private const string fixStr = "FPS: ";
        public FPS()
        {
            currentTime = 0;
            frameTick = 0;
            physicsTick = 0;

            Font font = new Font("Thasadith-Regular.ttf");
            text = new Text(fixStr, font, 35);
            text.Position = new Vector2f(20, 20);
            text.FillColor = Color.Black;
        }

        // นับ frame display rate ไปด้วย
        // แสดงผล update ทุก 1 วินาที
        public override void FrameUpdate(float deltaTime)
        {
            frameTick++;
        }

        public override void PhysicsUpdate(float fixTime)
        {
            physicsTick++;

            currentTime += fixTime;
            if (currentTime >= 1.0f)
            {
                string rate2 = fixStr + MathF.Round(frameTick / currentTime).ToString("N0") +
                    "  PhysicsUpdate: " + MathF.Round(physicsTick / currentTime).ToString("N0")
                    ;
                text.DisplayedString = rate2;
                currentTime = 0f;
                frameTick = 0;
                physicsTick = 0;
            }
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            text.Draw(target, states);
        }
    }
}
