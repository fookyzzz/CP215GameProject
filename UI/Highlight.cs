using GameLib;
using SFML.Graphics;
using SFML.System;

namespace GameLib
{
    public class Highlight : BlankEntity
    {
        public Vector2i index { get; set; } = new Vector2i(-1, -1);
        RectangleEntity rect;
        int tileSize;
        public Highlight(int tileSize)
        {
            this.tileSize = tileSize;
            rect = new RectangleEntity(new Vector2f(tileSize, tileSize));
            rect.Origin = new Vector2f(tileSize / 2, tileSize / 2);
            rect.OutlineColor = Color.Red;
            rect.OutlineThickness = 2;
            rect.FillColor = Color.Transparent;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            base.Draw(target, states);
            if (index.X < 0)
                return;

            rect.Position = (Vector2f)index * tileSize;
            rect.Draw(target, states);
        }

    }
}
