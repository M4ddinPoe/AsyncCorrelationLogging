using System;

namespace Calculation.PrimeNumbers.WorkloadGenerator
{
    internal sealed class DistributedWorkload
    {
        public DistributedWorkload(Guid id, DateTime distributedDate, int rangeStart, int rangeEnd)
        {
            this.Id = id;
            this.DistributedDate = distributedDate;
            this.RangeStart = rangeStart;
            this.RangeEnd = rangeEnd;
            this.Status = WorkloadStatus.Created;
        }

        public Guid Id { get; }

        public DateTime DistributedDate { get; }

        public int RangeStart { get; }

        public int RangeEnd { get; }

        public WorkloadStatus Status { get; }

        public static DistributedWorkload Initial
        {
            get
            {
                return new DistributedWorkload(Guid.Empty, DateTime.MinValue, 1, 1);
            }
        }
    }
}
