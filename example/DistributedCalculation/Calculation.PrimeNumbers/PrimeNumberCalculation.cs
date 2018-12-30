namespace Calculation.PrimeNumbers
{
  using System;

  using Calcuation.Contracts;

  public class PrimeNumberCalculation : ICalcuation<int, int>
  {
    public int Calculate(int workload)
    {
      if (workload <= 1)
      {
        return -1;
      }

      if (workload == 2)
      {
        return workload;
      }

      if (workload % 2 == 0)
      {
        return -1;
      }

      var boundary = (int)Math.Floor(Math.Sqrt(workload));

      for (int i = 3; i <= boundary; i += 2)
      {
        if (workload % i == 0)
        {
          return -1;
        }
      }

      return workload;
    }
  }
}
