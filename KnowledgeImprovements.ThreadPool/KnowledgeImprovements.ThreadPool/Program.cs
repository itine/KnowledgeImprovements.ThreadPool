using System;

namespace KnowledgeImprovements.ThreadPool
{
    class Program
    {
        static void Main(string[] args)
        {
            var tp = new MyThreadPool(CancellationToken.None);
            var l = new ThreadPoolListener(tp);
            for (var i = 0; i < 100; i++)
            {
                tp.AddAction(() => { i++; }, CancellationToken.None);
            }
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
