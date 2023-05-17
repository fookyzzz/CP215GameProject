using SFML.Graphics;
using SFML.System;

namespace GameLib
{
    public class Button : GenericButton
    {
        public string Text { get { return text.DisplayedString; } }
        Text text;
        RectangleShape? background;
        public Button(Vector2f position, string str, Font font, int charSize, 
            Vector2f buttonSize) : base(buttonSize)
        {
            Position = position;
            text = new Text(str, font, (uint)charSize);
            text.FillColor = Color.Black;
            text.Origin = text.TotalSize() / 2;
            text.Position = this.buttonSize / 2;

            UpdateVisual(ButtonState.Normal);
        }
        private void CreateBackgroundRect(Color fillColor, Color outlineColor)
        {
            var rect = new RectangleShape(buttonSize);
            rect.FillColor = fillColor;
            rect.OutlineColor = outlineColor;
            rect.OutlineThickness = 1;
            background = rect;
        }
        protected override void UpdateVisual(ButtonState state)
        {

            Color bgNormal = new Color(225, 225, 225);
            Color bgHighlight = new Color(229, 241, 251);
            Color bgPressed = new Color(204, 228, 247);
            Color lnNormal = new Color(173, 173, 173);
            Color lnHighlight = new Color(0, 120, 215);
            Color lnPressed = new Color(0, 84, 153);

            if (state == ButtonState.Pressed)
                CreateBackgroundRect(bgPressed, lnPressed);
            else if (state == ButtonState.Highlight)
                CreateBackgroundRect(bgHighlight, lnHighlight);
            else
                CreateBackgroundRect(bgNormal, lnNormal);
        }
        public override void Draw(RenderTarget target, RenderStates states)
        {
            base.Draw(target, states);
            states.Transform *= this.Transform;
            target.Draw(background, states);
            target.Draw(text, states);
        }
    }
}
