using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
