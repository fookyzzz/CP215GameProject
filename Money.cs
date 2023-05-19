using SFML.Graphics;
using SFML.System;
using System;

namespace GameLib
{
    public class Money : BlankTransformableEntity
    {
        public int MoneyValue { get; set; }
        public int CharSize { get; set; } = 50;
        public Font Font { get; set; }
        public Color BackColor { get; set; } = Color.Transparent;
        public Color FillColor { get; set; } = Color.White;
        public Money()
        {
            Font = FontCache.Get("../../../Resource/DSN_Sukumwit.ttf");
        }
        public void Clear()
        {
            MoneyValue = 0;
        }
        public void Increment(int delta)
        {
            MoneyValue += delta;
        }

        public bool CheckBeforeDecrement(int delta)
        {
            if (MoneyValue - delta < 0)
                return false;
            Decrement(delta);
            return true;
        }

        public void Decrement(int delta)
        {
            MoneyValue -= delta;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            base.Draw(target, states);
            var text = new Label("Money: " + String.Format("{0,6}", MoneyValue), Font, (uint)CharSize)
            {
                TextColor = this.FillColor,
                Position = new Vector2f(1280 / 9, 720 / 48),
                BgColor = this.BackColor,
            };
            text.Draw(target, states);
        }
    }
}
