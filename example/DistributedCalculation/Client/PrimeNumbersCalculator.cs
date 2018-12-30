using AsyncCorrelationLogging;
using Calcuation.Contracts;
using Calculation.PrimeNumbers;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    public class PrimeNumbersCalculator
    {
        private readonly int numberOfWorkers;

        public PrimeNumbersCalculator(int numberOfWorkers)
        {
            this.numberOfWorkers = numberOfWorkers;
        }

        public void Start(CancellationToken cancellationToken)
        {
            PrimeNumberCalculationWorker primeNumberCalculationWorker = new PrimeNumberCalculationWorker();
            primeNumberCalculationWorker.Start(cancellationToken);
        }
    }

    internal class PrimeNumberCalculationWorker
    {
        private RestClient client;

        private readonly Logger logger;

        public PrimeNumberCalculationWorker()
        {
            this.logger = new Logger();
            this.client = new RestClient(new Uri("http://localhost:58129/api"));
        }

        public void Start(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                this.ProcessPrimeNumbers();
            }
        }

        private void ProcessPrimeNumbers()
        {
            var workload = this.GetWorkload();
            var results = this.Calculate(workload);
            SendResults(workload, results);
        }

        private IWorkload<int> GetWorkload()
        {
            this.logger.Log("Get workload");
            RestRequest request = new RestRequest(new Uri("/prime-calculation", UriKind.Relative), Method.GET);
            IRestResponse<PrimeNumberWorkload> response = client.Execute<PrimeNumberWorkload>(request);

            this.logger.Log("Got new workload");

            return response.Data;
        }

        private IEnumerable<int> Calculate(IWorkload<int> workload)
        {
            List<int> primeNumbers = new List<int>();

            foreach (int number in workload.GetWorkload())
            {
                if (IsPrime(number))
                {
                    this.logger.Log($"Found prime number: {number}");
                    primeNumbers.Add(number);
                }
            }

            return primeNumbers;
        }

        private void SendResults(IWorkload<int> workload, IEnumerable<int> results)
        {
            PrimeNumbersResult primeNumbersResult = new PrimeNumbersResult
            {
                WorkloadId = workload.Id,
            };

            primeNumbersResult.Results.AddRange(results);

            IRestRequest request = new RestRequest(new Uri("/prime-calculation", UriKind.Relative), Method.POST, DataFormat.Json).AddBody(primeNumbersResult);
            IRestResponse response = client.Execute<PrimeNumberWorkload>(request);

            this.logger.Log($"Sent {results.Count()} prime numbers to server.");
        }

        public static bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            var boundary = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 3; i <= boundary; i += 2)
                if (number % i == 0)
                    return false;

            return true;
        }
    }
}
