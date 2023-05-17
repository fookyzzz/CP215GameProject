using SFML.Graphics;

namespace GameLib
{
    public static class ViewExtension
    {
        public static FloatRect GetRect(this View view)
        {
            return new FloatRect(view.Center - view.Size / 2, view.Size);
        }
    }
}