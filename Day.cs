using SFML.Graphics;
using SFML.System;
using System;

namespace GameLib
{
    public class Day : BlankTransformableEntity
    {
        public int DayValue { get; set; } = 1;
        public int CharSize { get; set; } = 50;
        public Font Font { get; set; }
        public Color BackColor { get; set; } = Color.Transparent;
        public Color FillColor { get; set; } = Color.White;
        public Day()
        {
            Font = FontCache.Get("../../../Resource/DSN_Sukumwit.ttf");
        }
        public void Clear()
        {
            DayValue = 1;
        }
        public void Increment()
        {
            DayValue += 1;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            base.Draw(target, states);
            var text = new Label("Day: " + String.Format("{0,3}", DayValue), Font, (uint)CharSize)
            {
                TextColor = this.FillColor,
                Position = new Vector2f(1280 / 1.12f, 720 / 48),
                BgColor = this.BackColor,
            };
            text.Draw(target, states);
        }
    }
}
