using GameLib;
using SFML.Graphics;
using SFML.System;

namespace GameLib
{
    public class Cross : BlankTransformableEntity
    {
        float size = 5;
        Color color = Color.Black;

        public Cross(Vector2f position, float size, Color color)
        {
            this.Position = position;
            this.size = size;
            this.color = color;
        }
        public override void Draw(RenderTarget target, RenderStates states)
        {
            VertexArray v = new VertexArray(PrimitiveType.Lines, 4);
            v[0] = new Vertex(new Vector2f(0.5f, -size), color);
            v[1] = new Vertex(new Vector2f(0.5f, size + 1), color);
            v[2] = new Vertex(new Vector2f(size + 1, 0.5f), color);
            v[3] = new Vertex(new Vector2f(-size, 0.5f), color);

            // ทำ this.Transform ก่อน แล้วตามด้วย states.Transform
            states.Transform = states.Transform * this.Transform;
            target.Draw(v, states);
        }
    }
}
