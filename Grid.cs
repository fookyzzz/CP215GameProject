using SFML.Graphics;
using SFML.System;

namespace GameLib
{
    public class Grid : BlankTransformableEntity
    {
        VertexArray array;
        public Grid(Vector2f size, int countX, int countY, Color color)
        {
            array = new VertexArray(PrimitiveType.Lines, (uint)(countX+1 + countY+1) * 2);

            for(uint i=0;i<=countX; i++)
            {
                var start = new Vector2f(size.X * i, 0);
                var end = start + new Vector2f(0, countY * size.Y);
                array[i*2] = new Vertex(start, color);
                array[i*2+1] = new Vertex(end, color);
            }

            uint startIndex = (uint)(countX+1) * 2;
            for (uint i = 0; i <= countY; i++)
            {
                var start = new Vector2f(0,size.Y * i);
                var end = start + new Vector2f(countX * size.X, 0);
                array[startIndex + i * 2] = new Vertex(start, color);
                array[startIndex + i * 2 + 1] = new Vertex(end, color);
            }
        }
        public override void Draw(RenderTarget target, RenderStates states)
        {
            base.Draw(target, states);
            states.Transform *= this.Transform;
            array.Draw(target, states);
        }
    }
}
