using Calcuation.Contracts;
using Calculation.PrimeNumbers;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Calculate prime numbers!");
            Console.WriteLine();
            Console.WriteLine("Number of Workers (5): ");
            string numberOfWorkersString = Console.ReadLine();

            int numberOfWorkers;

            if (!int.TryParse(numberOfWorkersString, out numberOfWorkers))
            {
                numberOfWorkers = 5;
            }

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            List<Task> tasks = new List<Task>();

            for (int index = 0; index < numberOfWorkers; index++)
            {
                Task task = Task.Run(() => StartCalculator(cancellationTokenSource.Token));
                tasks.Add(task);
            }

            Console.CancelKeyPress += (sender, e) =>
            {
                cancellationTokenSource.Cancel();
                Console.WriteLine("Stop requested");
            };

            Console.WriteLine("Calculation started");

            Task.WaitAll(tasks.ToArray());

            Console.WriteLine("Console stopeed");
            Console.ReadLine();
        }

        private static void StartCalculator(CancellationToken cancellationToken)
        {
            PrimeNumbersCalculator primeNumberCalculator = new PrimeNumbersCalculator(3);
            primeNumberCalculator.Start(cancellationToken);
        }
    }
}
