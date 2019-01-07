using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace ConsoleAppMultithreadingDemo
{
    class Demo
    {
        public void DoBackgroundTask()
        {
            WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} has a threadstate of " +
                $"{Thread.CurrentThread.ThreadState} with {Thread.CurrentThread.Priority} priority");
            WriteLine($"Start thread sleep at {DateTime.Now.Second} seconds");
            // prematurely end Thread
            Thread.CurrentThread.Abort();
            Thread.Sleep(3000);
            WriteLine($"End thread sleep at {DateTime.Now.Second} seconds");

        }

        public void IncreaseThreadpoolSize()
        {
            // max nr Processors
            int numberOfProcessors = Environment.ProcessorCount;
            WriteLine($"Processor Count = {numberOfProcessors}");

            // max nr Threads
            int maxWorkerThreads;
            int maxConcurrentActiveRequests;
            int minWorkerThreads;
            int minConcurrentActiveRequests;
            ThreadPool.GetMaxThreads(out maxWorkerThreads, out maxConcurrentActiveRequests);
            ThreadPool.GetMinThreads(out minWorkerThreads, out minConcurrentActiveRequests);
            WriteLine($"ThreadPool minimum Worker = {minWorkerThreads}" +
                $" and minimum Requests = {minConcurrentActiveRequests}");
            WriteLine($"ThreadPool maximum Worker = {maxWorkerThreads}" +
                $" and Requests = {maxConcurrentActiveRequests}");

            // random nr
            Random randomWorkers = new Random();
            int newMaxWorker = randomWorkers.Next(minWorkerThreads, maxWorkerThreads);
            WriteLine($"New Max Worker Thread generated = {newMaxWorker}");

            Random randomRequests = new Random();
            int newMaxRequests = randomRequests.Next(minConcurrentActiveRequests, maxConcurrentActiveRequests);
            WriteLine($"New Max Requests generated = {newMaxRequests}");

            // queue

            bool changeSucceeded = ThreadPool.SetMaxThreads(newMaxWorker, newMaxRequests);
            if (changeSucceeded)
            {
                WriteLine("SetMaxThreads completed");
                int maxWorkerCount;
                int maxActiveRequestsCount;
                ThreadPool.GetMaxThreads(out maxWorkerCount, out maxActiveRequestsCount);
                WriteLine($"ThreadPool Max Worker = {maxWorkerCount}" +
                    $" and Max Requests = {maxActiveRequestsCount}");
            }
            else
                WriteLine("SetMaxThreads failed");





        }

        public void MultiplethreadWait()
        {
            Task thread1 = Task.Factory.StartNew(() => RunThread(3));
            Task thread2 = Task.Factory.StartNew(() => RunThread(5));
            Task thread3 = Task.Factory.StartNew(() => RunThread(2));

            Task.WaitAll(thread1, thread2, thread3);
            WriteLine("All tasks completed");
        }

        public void RunThread(int sleepSeconds)
        {
            int threadID = Thread.CurrentThread.ManagedThreadId;

            WriteLine($"Sleep thread {threadID} for {sleepSeconds}" +
                $" seconds at {DateTime.Now.Second} seconds");
            Thread.Sleep(sleepSeconds * 1000);
            WriteLine($"Wake thread {threadID} at {DateTime.Now.Second} seconds");

        }

        // Locking one Thread until the contended resources are available
        /// <summary>
        /// Best practice defining the object to lock on as private
        /// </summary>
        private object threadlock = new object();

        public void LockThreadExample()
        {
            Task thread1 = Task.Factory.StartNew(() => ContendedResource(3));
            Task thread2 = Task.Factory.StartNew(() => ContendedResource(5));
            Task thread3 = Task.Factory.StartNew(() => ContendedResource(2));

            Task.WaitAll(thread1, thread2, thread3);
            WriteLine("All tasks completed");
        }

        private void ContendedResource(int sleepSeconds)
        {
            int threadID = Thread.CurrentThread.ManagedThreadId;

            lock (threadlock)
            {
                WriteLine($"Locked for thread {threadID}");
                Thread.Sleep(sleepSeconds * 1000);
            }

            WriteLine($"Lock released for {threadID} at {DateTime.Now.Second} seconds");
        }

        // Invoking parallel calls to methods using Parallel.Invoke

        public void ParallelInvoke()
        {
            WriteLine($"Parallel.Invoke started at {DateTime.Now.Second} seconds");

            Parallel.Invoke(
                () => PerformSomeTask(3),
                () => PerformSomeTask(5),
                () => PerformSomeTask(2)
             );

            WriteLine($"Parallel.Invoke completed at {DateTime.Now.Second} seconds");

        }

        private void PerformSomeTask(int sleepSeconds)
        {
            int threadID = Thread.CurrentThread.ManagedThreadId;

            WriteLine($"Sleep thread {threadID} for {sleepSeconds}" +
                $" seconds at {DateTime.Now.Second} seconds");
            Thread.Sleep(sleepSeconds * 1000);
            WriteLine($"Thread {threadID} resumed");
        }

        // Using a parallel foreach loop
        // when dealing with bulk processing

        public double ReadCollectionForeach(List<string> intCollection)
        {
            var timer = Stopwatch.StartNew();

            foreach (string integer in intCollection)
            {
                WriteLine(integer);
                Clear();
            }

            return timer.Elapsed.TotalSeconds;
        }
        
        public double ReadCollectionParallelForeach(List<string> intCollection)
        {
            var timer = Stopwatch.StartNew();

            Parallel.ForEach(intCollection, integer =>
            {
                WriteLine(integer);
                Clear();
            });

            return timer.Elapsed.TotalSeconds;
        }

        public void CreateWriteFilesForEach(List<string> intCollection)
        {
            WriteLine("Start foreach File method");

            var timer = Stopwatch.StartNew();

            foreach (string integer in intCollection)
            {
                string filePath = $"C:\\temp\\output\\ForEach_Log{integer}.txt";

                if(!File.Exists(filePath))
                {
                    File.Create(filePath).Dispose();
                    using (StreamWriter sw = new StreamWriter(filePath, false))
                    {
                        sw.WriteLine($"{integer}. Log file start: {DateTime.Now.ToUniversalTime().ToString()}");
                    
                    }     

                }
            }

            WriteLine($"Foreach File method executed in {timer.Elapsed.TotalSeconds} seconds");
        }

        public void CreateWriteFilesParallelForEach(List<string> intCollection)
        {
            WriteLine("Start Parallel foreach File method");

            var timer = Stopwatch.StartNew();

            Parallel.ForEach(intCollection, integer =>
            {

                string filePath = $"C:\\temp\\output\\ParallelForEach_Log{integer}.txt";

                if (!File.Exists(filePath))
                {
                    File.Create(filePath).Dispose();
                    using (StreamWriter sw = new StreamWriter(filePath, false))
                    {
                        sw.WriteLine($"{integer}. Log file start: {DateTime.Now.ToUniversalTime().ToString()}");

                    }

                }
            });

            WriteLine($"Parallel foreach File method executed in {timer.Elapsed.TotalSeconds} seconds");
        }
    }
}
