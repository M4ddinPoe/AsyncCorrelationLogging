namespace Calculation.PrimeNumbers.WorkloadGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Calcuation.Contracts;
    using PrimeNumbers;

    public sealed class PrimeNumberGenerator
    {
        private const int WorkloadSize = 1000;

        private DistributedWorkload _lastDistributedWorkload = DistributedWorkload.Initial;

        public async Task<IWorkload<int>> GetNextWorkloadAsync()
        {
            return await Task.Run(() => GetNextWorkload());
        }
  
        public IWorkload<int> GetNextWorkload()
        {
            List<int> numbers = new List<int>();
            int start = this._lastDistributedWorkload.RangeEnd;
            int end = start + WorkloadSize;

            for (int current = start; current < end; current++)
            {
                numbers.Add(current);
            }

            this._lastDistributedWorkload = new DistributedWorkload(
              Guid.NewGuid(),
              DateTime.Now,
              start,
              start + WorkloadSize);

            var workload = new PrimeNumberWorkload(this._lastDistributedWorkload.Id, numbers);

            return workload;
        }
    }
}
