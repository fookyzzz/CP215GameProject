
using GameLib;

namespace GameLib
{
    public interface Task : Entity
    {
        Task Start();
        bool IsStop();
    }
}
