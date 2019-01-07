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
            #region Backgroundthread
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
            #endregion
            #region ThreadPoolSize
            // Increase ThreadpoolSize
            /*demo.IncreaseThreadpoolSize();
            Console.ReadLine();
            */
            #endregion
            #region Multiple Threads
            // Using multiple threads
            /*
            demo.MultiplethreadWait();
            Console.ReadLine();
            */
            #endregion
            #region Locking
            // Locking threads
            /*
            demo.LockThreadExample();
            Console.ReadLine();
            */
            #endregion
            #region Parallel Invoke
            // Using parallel Invoke
            /*
            demo.ParallelInvoke();
            Console.ReadLine();
            */
            #endregion
            // Foreach VS Parallel Foreach

            List<string> intCollection = new List<string>();
            for (int i = 0; i < 500; i++)
            {
                intCollection.Add(i.ToString());
            }
            double readCollectionForeach = demo.ReadCollectionForeach(intCollection);
            double readCollectionParallelForeach = demo.ReadCollectionParallelForeach(intCollection);

            Console.WriteLine($"Foreach finished in {readCollectionForeach}");
            Console.WriteLine($"Parallel foreach finished in {readCollectionParallelForeach}");
            Console.ReadLine();
        }
    }
}
