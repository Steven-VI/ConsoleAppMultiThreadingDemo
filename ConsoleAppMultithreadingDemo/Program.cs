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
            #region Simple Foreach VS Parallel Foreach
            // Foreach VS Parallel Foreach
            /*
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
            */
            #endregion
            #region Intensive Foreach VS Parallel Foreach
            //Foreach VS Parallel Foreach
            /*
            List<string> intCollection = new List<string>();

            for (int i = 0; i < 1000; i++)
            {
                intCollection.Add(i.ToString());
            }

            demo.CreateWriteFilesForEach(intCollection);
            demo.CreateWriteFilesParallelForEach(intCollection);
            Console.ReadLine();
            */
            #endregion
            #region Cancel Parallel Loop
            // Cancel Parallel loop
            /*List<string> intCollection = new List<string>();
            for (int i = 0; i < 1000; i++)
            {
                intCollection.Add(i.ToString());
            }

            demo.CancelParallelForEach(intCollection, 3);
            Console.WriteLine("Parallel.ForEach loop terminated");
            Console.ReadLine();
            */
            #endregion
            // Catch error parallel foreach

            List<string> ipAddresses = new List<string>();

            for (int i = 0; i < 11; i++)
            {
                ipAddresses.Add($"10.0.0.{i.ToString()}");
            }

            try
            {
                demo.CheckClientMachinesOnline(ipAddresses, 0);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
            }

            Console.ReadLine();
        }
    }
}
