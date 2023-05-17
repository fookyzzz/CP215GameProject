
using GameLib;

namespace GameLib
{
    public interface Task : Entity
    {
        void Start();
        bool IsFinish();
    }
}
