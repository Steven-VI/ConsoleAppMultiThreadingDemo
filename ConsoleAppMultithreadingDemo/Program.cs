using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleAppMultithreadingDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Demo demo = new Demo();
            // Add backgroundthread and threadpriority
            /*
            var backgroundThread = new Thread(demo.DoBackgroundTask)
            {
                IsBackground = true,
                Priority = ThreadPriority.Lowest
            };
            backgroundThread.Start();
            Thread.Sleep(5000);
            */
            // Increase ThreadpoolSize
            /*demo.IncreaseThreadpoolSize();
            Console.ReadLine();
            */
            // Using multiple threads
            /*
            demo.MultiplethreadWait();
            Console.ReadLine();
            */
            // Locking threads
            /*
            demo.LockThreadExample();
            Console.ReadLine();
            */
            demo.ParallelInvoke();
            Console.ReadLine();
        }
    }
}
