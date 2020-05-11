using System;
using System.Collections.Concurrent;
using System.Threading;

namespace KnowledgeImprovements.ThreadPool
{
    public class MyThreadPool
    {
        private readonly BlockingCollection<ActionInfo> _actions = new BlockingCollection<ActionInfo>();
        private readonly Thread _backgroundThread;
        public event EventHandler<MyThreadInfoEventArgs> OnFinishedActionEvent;

        public MyThreadPool(CancellationToken ct)
        {
            _backgroundThread = new Thread(() => StartActionsProcessing(ct));
            _backgroundThread.Start();
        }

        public void AddAction(Action action, CancellationToken ct) =>
            _actions.Add(new ActionInfo
            {
                Guid = Guid.NewGuid(),
                Action = action
            }, ct);

        public void StartActionsProcessing(CancellationToken ct)
        {
            try
            {
                foreach (var actionInfo in _actions.GetConsumingEnumerable(ct))
                {
                    try
                    {
                        Thread.Sleep(1000);
                        actionInfo.Action();
                        ThreadPoolInfo($"Action { actionInfo.Guid } completed");
                    }
                    catch (Exception ex)
                    {
                        ThreadPoolInfo($"Action { actionInfo.Guid } failed, {ex}");
                    }
                }
            }
            catch (Exception ex)
            {
                ThreadPoolInfo($"GetConsumingEnumerable failed, {ex}");
            }

        }

        public void StopActionsProcessing()
        {
            _actions.CompleteAdding();
            _backgroundThread.Join();
        }

        protected virtual void ThreadPoolInfo(string message) =>
            OnFinishedActionEvent?.Invoke(this, new MyThreadInfoEventArgs(message));
    }

    public class MyThreadInfoEventArgs : EventArgs
    {
        public Object Message { get; }
        public MyThreadInfoEventArgs(Object obj)
        {
            Message = obj;
        }
    }
}
