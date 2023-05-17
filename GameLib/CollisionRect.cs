using SFML.Graphics;
using SFML.System;
using System;

namespace GameLib
{
    // concrete obj ของ CollisionShape
    // สิ่งที่ต้องมีให้คือ GlobalTransformProvider 
    // สิ่งที่มีให้คือ บอกได้ว่าชนกับ shape อื่นหรือไม่
    public class CollisionRect
    {
        private FloatRect rect;
        public Entity? Parent;
        private FloatRect GlobalRect => Parent!.GlobalTransform.TransformRect(rect);

        [Obsolete]
        public CollisionRect(FloatRect rect, Entity parent)
        {
            this.rect = rect;
            this.Parent = parent;
        }
        public CollisionRect(FloatRect rect)
        {
            this.rect = rect;
            this.Parent = null; // จะถูกตั้งค่าจาก CollisionObj
        }
        public bool IsCollide(CollisionRect shapeB, out CollideData collideData)
        {
            var rectA = this.GlobalRect;
            var rectB = shapeB.GlobalRect;

            collideData = new CollideData();
            return rectA.Intersects(rectB, out collideData.OverlapRect);
        }

        public Vector2f RelativeDirection(FloatRect overlap)
        {
            return GlobalRect.RelativeDirection(overlap);
        }


        // จะไม่สนใจค่า states.Transform เพราะจะวาดใน global coordinate อยู่แล้ว
        public void DebugDraw(RenderTarget target)
        {
            var shape = CreateRectangleShape(GlobalRect);
            shape.FillColor = Color.Transparent;
            shape.OutlineColor = Color.Red;
            shape.OutlineThickness = 1;
            shape.Draw(target, RenderStates.Default);
        }

        private static RectangleShape CreateRectangleShape(FloatRect rect)
        {
            var shape = new RectangleShape(new Vector2f(rect.Width, rect.Height));
            shape.Position = new Vector2f(rect.Left, rect.Top);
            return shape;
        }

    }
}
