using System;

namespace KnowledgeImprovements.ThreadPool
{
    public class ThreadPoolListener : IDisposable
    {
        private readonly MyThreadPool _threadPool;
        public ThreadPoolListener(MyThreadPool threadPool)
        {
            _threadPool = threadPool;
            _threadPool.OnFinishedActionEvent += ShowFinishedInfo;
        }

        private void ShowFinishedInfo(Object sender, MyThreadInfoEventArgs e) =>
            Console.WriteLine($"{e.Message}");

        public void Dispose()
        {
            _threadPool.OnFinishedActionEvent -= ShowFinishedInfo;
        }
    }
}
