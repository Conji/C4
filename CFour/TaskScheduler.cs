using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFour
{
    public class TaskScheduler
    {
        public CFour CFour { get; }
        private TaskFactory m_TaskFactory;

        public TaskScheduler(CFour c4)
        {
            CFour = c4;
            m_TaskFactory = new TaskFactory(TaskCreationOptions.HideScheduler, TaskContinuationOptions.NotOnCanceled);
        }

        public void Schedule(Action<CFour> task)
        {
            m_TaskFactory.StartNew(() => task?.Invoke(CFour));
        }
    }
}
