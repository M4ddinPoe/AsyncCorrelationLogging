using System;
using System.Collections.Generic;
using Calcuation.Contracts;

namespace Calculation.PrimeNumbers
{
    public class PrimeNumbersResult : ICalculationResult
    {
        public Guid WorkloadId { get; set; }

        public List<int> Results { get; set; } = new List<int>();
    }
}
