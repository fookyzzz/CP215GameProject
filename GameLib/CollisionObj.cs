using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;

namespace GameLib
{
    // Collision Detection Unit จะค้นหาทุก obj ใน tree ที่ implement interface นี้
    // แล้วเอามาเช็คการชนกัน
    //   - แจ้งการชนที่ OnCollide(...)
    // แบบ basic ประกอบด้วย shape เดียว
    public class CollisionObj : BlankEntity
    {
        public CollisionRect Shape { get; private set; }
        public int GroupCode { get; set; } = 0;
        public List<CollisionObj> LastObjList = new List<CollisionObj>();
        public List<CollisionObj> CurrentObjList = new List<CollisionObj>();
        public bool DebugDraw { get; set; } = false;
        public delegate void OnCollideDelegate(CollisionObj objB, CollideData collideData);
        public event OnCollideDelegate OnCollide = delegate { };

        public CollisionObj(CollisionRect shape)
        {
            Shape = shape;
            shape.Parent = this;
        }
        public CollisionObj(CollisionRect shape, int groupCode)
        {
            Shape = shape;
            GroupCode = groupCode;
        }

        public void InvokeCollide(CollisionObj objB, CollideData collideData)
        {
            OnCollide(objB, collideData);
        }

        public Vector2f RelativeDirection(FloatRect overlap)
        {
            return Shape.RelativeDirection(overlap);
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            base.Draw(target, states);
            if(DebugDraw)
            {
                Shape.DebugDraw(target);
            }
        }
    }
}
