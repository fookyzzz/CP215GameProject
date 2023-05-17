using SFML.Graphics;
using SFML.System;

namespace GameLib
{
    public class Label : Group
    {
        public Font Font { get => _font; set { _font = value; Update(); } }
        Font _font;
        public Color TextColor { get => textColor; set { textColor = value; Update(); } }
        Color textColor = Color.Blue;
        public Color BgColor { get => bgColor; set { bgColor = value; Update(); } }
        Color bgColor = Color.White;
        public float HMargin { get => hMargin; set { hMargin = value; Update(); } }
        float hMargin = 0;
        public float VMargin { get => vMargin; set { vMargin = value; Update(); } }
        float vMargin = 0;
        public string StrText { get { return str;  } set { str = value; Update(); } }
        string str;

        public uint FontSize { get => fontSize; set { fontSize = value; Update(); } }
        uint fontSize;

        bool sizeBasedOnText;
        float bgHSize;

        //public TextEntity TextEntity;
        //public Shape BgShape;
        public Label(string str, Font font, uint fontSize)
        {
            _font = font;
            this.str = str;
            this.fontSize = fontSize;
            sizeBasedOnText = true;
            Update();
        }
        public Label(string str, Font font, uint fontSize, float horizontalSize)
        {
            _font = font;
            this.str = str;
            this.fontSize = fontSize;
            sizeBasedOnText = false;
            this.bgHSize = horizontalSize;
            Update();
        }
        private void Update()
        {
            this.Clear();
            var text = new TextEntity(StrText, _font, fontSize);
            text.FillColor = TextColor;

            text.Position += new Vector2f(HMargin, VMargin);

            float bgHorSize;
            if (sizeBasedOnText)
                bgHorSize = text.TotalWidth() + 2 * HMargin;
            else
                bgHorSize = this.bgHSize;
            var rect = new RectangleEntity(
                            new Vector2f(bgHorSize,
                                        text.TotalHeight() + 2 * VMargin));
            rect.FillColor = BgColor;

            this.Add(rect);
            this.Add(text);
            //DebugPositions(text);
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
