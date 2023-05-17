using SFML.Graphics;
using SFML.System;

namespace GameLib
{
    public class Label : Group
    {
        public string FontName { get; init; } = "Thasadith-Regular.ttf";
        public Color TextColor { get; init; } = Color.Blue;
        public Color BgColor { get; init; } = Color.White;
        public float HMargin { get; init; } = 0;
        public float VMargin { get; init; } = 0;
        public Label(string str, string fontName, uint fontsize)
        {
            this.FontName = fontName;
            Create(str, fontsize);
        }
        public Label(string str, uint fontsize)
        {
            Create(str, fontsize);
        }
        private void Create(string str, uint fontsize)
        {
            this.Clear();
            var font = FontCache.Get(FontName);
            var text = new TextEntity(str, font, fontsize);
            text.FillColor = TextColor;
            text.Position += new Vector2f(HMargin, VMargin);

            var rect = new RectangleEntity(
                            new Vector2f(text.TotalWidth() + 2 * HMargin, 
                                        text.TotalHeight() + 2 * VMargin));
            rect.FillColor = BgColor;

            this.Add(rect);
            this.Add(text);
            DebugPositions(text);
        }

        private void DebugPositions(TextEntity text)
        {
            float width = text.TotalWidth();
            float height = text.TotalHeight();
            float chSize = text.BaselineHeight();
            this.Add(new Cross(new Vector2f(width, 0), 5, Color.Black));
            this.Add(new Cross(new Vector2f(width, chSize), 5, Color.Red));
            this.Add(new Cross(new Vector2f(width, height), 5, Color.Blue));
        }
    }


    //var bound = text.GetLocalBounds(); // ความสูงมักไม่ได้ สำหรับ font ไทย, ความกว้างก็มักพอดีตัวเกินไป
}
