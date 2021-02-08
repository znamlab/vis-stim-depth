using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

//NOTES:
/*
 * 1. IObservable<T>: a reactive sequence of things; 
 *      Push: the numbers will be pushed to me when it's ready and it may not be immediately ready (rather than pulling from a static source)
 *              so the data source is in charge of when the program gets the data
 *      Pull: we have a pool of numbers ready, and the code is in charge of when it retrieves number from that pool
 * 2. 
 */

namespace RxDemo1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //1) Create an array
            /*
            IObservable<int> xs = Observable.Range(1, 10);
            xs.Subscribe(x => Console.WriteLine(x));
            */

            //2) Create a Timer
            //static async Task Main(string[] args)
            //async method runs synchronisely until it reaches its first await expression, when the method is suspended until the awaited task is complete
            /*
            IObservable<long> ts = Observable.Timer(DateTimeOffset.Now.AddSeconds(1.5),TimeSpan.FromSeconds(0.5));
            ts.Subscribe(t => Console.WriteLine(t*2));

            await new TaskCompletionSource<object>().Task; //the task never stops until you terminates it
            */

            //3) .Methods
            /*
                IObservable<long> ts = Observable
                    .Timer(DateTimeOffset.Now.AddSeconds(1.5), TimeSpan.FromSeconds(0.5))
                    .Where(t => t%2==0) //when t is divisable by 2
                    .Take(5); //Takes the first five items from a sequence
                ts.Subscribe(t => Console.WriteLine(t * 2));

                await new TaskCompletionSource<object>().Task; //the task never stops until you terminates it
            */

            //4) 
            var chars = Keystrokes();
            chars.Subscribe(achar => Console.WriteLine(achar));
            await new TaskCompletionSource<object>().Task;

        }

        static IObservable<char> Keystrokes()
        {
            return Observable.Create<char>(
                obs =>
                {
                    return Task.Run(() =>
                    {
                        obs.OnNext('h');
                        obs.OnNext('e');
                        obs.OnNext('l');
                        obs.OnNext('l');
                        obs.OnNext('o');
                    }
                    );

                }
                );
        }
    }
}
