namespace Calculation.PrimeNumbers
{
    using System;
    using System.Collections.Generic;

    using Calcuation.Contracts;

    public class PrimeNumberWorkload : IWorkload<int>
    {
        public PrimeNumberWorkload()
        {
            this.Id = Guid.Empty;
            Numbers = new int[0];
        }

        public PrimeNumberWorkload(Guid id, IEnumerable<int> numbers)
        {
            this.Numbers = numbers;
            this.Id = id;
        }

        public Guid Id { get; set; }

        public IEnumerable<int> Numbers { get; set; }

        public IEnumerable<int> GetWorkload()
        {
            return this.Numbers;
        }
    }
}
