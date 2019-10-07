using System;
using System.Reactive.Subjects;

namespace ReactiveProgrammingApp
{
    public class Program : IObserver<float>
    {
        public Program()
        {
            var market = new Subject<float>();
            market.Subscribe(this);

            market.OnNext(1.24f);
        }

        public void OnNext(float value)
        {
            Console.WriteLine($"Market gave us {value}");
        }

        public void OnError(Exception error)
        {
            Console.WriteLine($"We got an error {error.Message}");
        }

        public void OnCompleted()
        {
            Console.WriteLine($"Sequence is complete");
        }

        private static void Main(string[] args)
        {
            var program = new Program();
            Console.ReadLine();
        }
    }
}