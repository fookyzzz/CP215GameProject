using System;
using System.Collections.Generic;

namespace GameLib
{
    public class SequentialTask : EventForwarder, Task
    {
        private Task[] tasks;
        private int current = -1;

        public SequentialTask(params Task[] tasks)
        {
            this.tasks = tasks;
        }

        public void Add(Task task)
        {
            Task[] newTask = new Task[tasks.Length + 1];
            Array.Copy(tasks, newTask, tasks.Length);
            newTask[tasks.Length] = task;
            tasks = newTask;
        }

        public Task Start()
        {
            current = 0;
            if (tasks.Length > 0)
            {
                Task t = tasks[0];
                this.forwardToObj = t;
                t.Start();
            }
            return this;
        }

        public override void PhysicsUpdate(float fixTime)
        {
            NextTask();
            base.PhysicsUpdate(fixTime);
        }

        private void NextTask()
        {
            if (IsStop())
                return;
            if (current >= tasks.Length)
                return; // finish

            if (tasks[current].IsStop())
            {
                current++;
                if (current >= tasks.Length)
                    this.forwardToObj = null; // finished
                else
                {
                    Task t = tasks[current];
                    this.forwardToObj = t;
                    t.Start();
                }
            }
        }

        public bool IsStop()
        {
            return current == -1 || current >= tasks.Length;
        }
    }
}
