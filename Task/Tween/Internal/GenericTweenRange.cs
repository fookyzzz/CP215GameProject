using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib
{
    public interface GenericTweenRange<T>
    {
        public T GetCurrentValue(float accumulatedTime);
        public T EndValue { get; }
        public float Duration { get; }

    }
}
