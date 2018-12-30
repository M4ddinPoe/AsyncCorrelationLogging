using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Calculation.PrimeNumbers.WorkloadGenerator.Test
{
  using Calcuation.Contracts;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;

  [TestClass]
  public partial class PrimeNumberGeneratorTest : IDisposable
  {
    private Fixture fixture;

    [TestInitialize]
    public void Setup()
    {
      this.fixture = new Fixture();
    }

    [TestCleanup]
    public void Dispose()
    {
      if (this.fixture != null)
      {
        this.fixture.Dispose();
        this.fixture = null;
      }
    }

    [TestMethod]
    public async Task GetNextWorkload_ShouldReturnExpectedEntities()
    {
      PrimeNumberGenerator testObject = this.fixture.CreateTestObject();

      IWorkload<int> workload = testObject.GetNextWorkload();
      IEnumerable<int> numbers = workload.GetWorkload();

      Assert.AreEqual(1000, numbers.Count());
      Assert.AreEqual(1, numbers.First());
      Assert.AreEqual(1000, numbers.Last());

      workload = testObject.GetNextWorkload();
      numbers = workload.GetWorkload();

      Assert.AreEqual(1000, numbers.Count());
      Assert.AreEqual(1001, numbers.First());
      Assert.AreEqual(2000, numbers.Last());
    }
  }
}
