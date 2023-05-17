using SFML.Graphics;
using SFML.System;

namespace GameLib
{
    public static class TextExtension
    {
        public static float BaselineHeight(this Text text)
        {
            return text.CharacterSize;
        }
        public static float TotalHeight(this Text text)
        {
            return text.Font.GetLineSpacing(text.CharacterSize);
        }
        public static float TotalWidth(this Text text)
        {
            return text.FindCharacterPos((uint)text.DisplayedString.Length).X;
        }
        public static Vector2f TotalSize(this Text text)
        {
            return new Vector2f(TotalWidth(text), TotalHeight(text));
        }
        public static FloatRect TotalRect(this Text text)
        {
            return new FloatRect(0, 0, text.TotalWidth(), text.TotalHeight());
        }
    }
}
