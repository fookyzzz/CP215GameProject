using SFML.Graphics;
using System;

namespace GameLib
{
    public class Score : BlankTransformableEntity
    {
        public int ScoreValue { get; set; }
        public int CharSize { get; set; } = 50;
        public Font Font { get; set; }
        public Color FillColor { get; set; } = Color.Black;
        public Score()
        {
            Font = FontCache.Get("../../../Resource/DSN_Sukumwit.ttf");
        }
        public void Clear()
        {
            ScoreValue = 0;
        }
        public void Increment(int delta)
        {
            ScoreValue += delta;
        }
        public override void Draw(RenderTarget target, RenderStates states)
        {
            base.Draw(target, states);
            var text = new TextEntity(String.Format("{0,6}", ScoreValue), Font, (uint)CharSize)
            {
                FillColor = this.FillColor,
            };
            text.Draw(target, states);
        }
    }
}
