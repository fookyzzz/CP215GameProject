using SFML.Graphics;
using SFML.System;

namespace GameLib
{
    public class ImageButton : GenericButton
    {
        public string Str { get { return text.DisplayedString; } }
        public Color TextColor { get => text.FillColor; set { text.FillColor = value; } }
        public Color HighlightColor { get; set; } = new Color(229, 241, 251);
        public Color PressedColor { get; set; } = new Color(204, 228, 247);

        Text text;
        SpriteEntity bgImage;
        public ImageButton(string str, Font font, int charSize, 
            SpriteEntity bgImage) : base(bgImage.GetGlobalBounds().GetSize())
        {
            this.bgImage = bgImage;
            text = new Text(str, font, (uint)charSize);
            text.FillColor = Color.Black;
            text.Origin = text.TotalSize() / 2;
            text.Position = this.buttonSize / 2;

            UpdateVisual(ButtonState.Normal);
        }
        protected override void UpdateVisual(ButtonState state)
        {
            if (state == ButtonState.Pressed)
                bgImage.Color = PressedColor;
            else if (state == ButtonState.Highlight)
                bgImage.Color = HighlightColor;
            else
                bgImage.Color = Color.White;
        }
        public override void Draw(RenderTarget target, RenderStates states)
        {
            base.Draw(target, states);
            states.Transform *= this.Transform;
            target.Draw(bgImage, states);
            target.Draw(text, states);
        }
    }
}
