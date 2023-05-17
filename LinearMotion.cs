using GameLib;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game11
{
    public class LinearMotion
    {
        Transformable? obj;
        Vector2f v;
        float t;

        public Vector2f GetNormalizedDirection()
        {
            return v.Normalize();
        }

        public LinearMotion(Transformable? obj, Vector2f v, float t)
        {
            this.obj = obj;
            this.v = v;
            this.t = t;
        }
        public LinearMotion(Transformable obj, float speed, Vector2f displacement)
        {
            Debug.Assert(speed > 0);
            this.obj = obj;
            Vector2f s = displacement;
            this.t = s.Size() / speed;
            if(t == 0.0f)
                v = new Vector2f(0, 0);
            else
                v = s / t;
        }

        public bool IsFinished()
        {
            return t <= 0;
        }

        public void Update(float fixTime)
        {
            if (t <= 0 || obj == null)
                return;

            obj.Position += v * fixTime;
            t -= fixTime;
        }

        public static LinearMotion Empty() 
        { 
            return new LinearMotion(null, new Vector2f(), 0); 
        }
    }
}
