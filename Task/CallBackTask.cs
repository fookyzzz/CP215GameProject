using GameLib;

namespace GameLib
{
    public class CallBackTask : BlankEntity, Task
    {
        private Trigger.VoidFunc func;
        private bool finish = false;

        public CallBackTask(Trigger.VoidFunc func)
        {
            this.func = func;
        }

        public void Start()
        {
            func();
            finish = true;
        }
        public bool IsFinish()
        {
            return finish;
        }
    }
}
