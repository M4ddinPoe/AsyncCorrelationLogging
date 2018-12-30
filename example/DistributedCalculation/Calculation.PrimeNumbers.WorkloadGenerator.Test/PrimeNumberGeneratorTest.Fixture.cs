namespace Calculation.PrimeNumbers.WorkloadGenerator.Test
{
  using System;
  using WorkloadGenerator;

  public partial class PrimeNumberGeneratorTest
  {
    private sealed class Fixture : IDisposable
    {
      public PrimeNumberGenerator CreateTestObject()
      {
        return new PrimeNumberGenerator();
      }

      public void Dispose()
      {
      }
    }
  }
}
