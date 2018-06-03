using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;

namespace Reactive
{
    internal class Program
    {
        private static Stopwatch _stopwatch;

        private static void Main(string[] args)
        {
            _stopwatch = new Stopwatch();
            _stopwatch.Start();

            Console.WriteLine("Thread {0}", Thread.CurrentThread.ManagedThreadId);

            var query = from number in Enumerable.Range(1, 5) select number;
            IEnumerable<int> enumerable = query as IList<int> ?? query.ToList();

            // Normal way
            //foreach (var number in enumerable)
            //    Console.WriteLine(number);
            //ImDone();

            // Reactive Way
            //var observableQuery = enumerable.ToObservable();
            //observableQuery.Subscribe(Console.WriteLine, ImDone);

            // Scheduler - run on a different thread
            // Manage concurrency
            // call i'm done when all thread's completed
            var observableQuery = enumerable.ToObservable(new NewThreadScheduler());

            observableQuery.Subscribe(ProcessThread, ImDone);
            
            Console.ReadKey();
        }

        public static void ProcessThread(int number)
        {
            Console.WriteLine("Sleeping for {0}", number);
            //Thread.Sleep(number);
            Console.WriteLine("{0} number Thread {1}", number, Thread.CurrentThread.ManagedThreadId);
        }

        public static void ImDone()
        {
            _stopwatch.Stop();
            Console.WriteLine("I'm done here");
            Console.WriteLine("Elapsed time {0}", _stopwatch.ElapsedMilliseconds);
        }
    }
}