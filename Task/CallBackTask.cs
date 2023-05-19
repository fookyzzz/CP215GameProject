using GameLib;

namespace GameLib
{
    public class CallBackTask : BlankEntity, Task
    {
        private Trigger.VoidFunc func;
        private bool stop = true;

        public CallBackTask(Trigger.VoidFunc func)
        {
            this.func = func;
        }

        public Task Start()
        {
            stop = false;
            func();
            stop = true;
            return this;
        }
        public bool IsStop()
        {
            return stop;
        }
    }
}
