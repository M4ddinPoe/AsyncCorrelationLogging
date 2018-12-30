using System.Collections.Generic;
using System.Threading.Tasks;
using Calculation.PrimeNumbers;
using Calculation.PrimeNumbers.WorkloadGenerator;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    [Route("api/prime-calculation")]
    public class PrimeCalculationController : Controller
    {
        private readonly PrimeNumberGenerator primeNumberGenerator;

        private static List<int> primeNumbers = new List<int>();

        public PrimeCalculationController(PrimeNumberGenerator primeNumberGenerator)
        {
            this.primeNumberGenerator = primeNumberGenerator;
        }

        [HttpGet]
        public async Task<OkObjectResult> GetAsync()
        {
            var workload = await primeNumberGenerator.GetNextWorkloadAsync();
            return Ok(workload);
        }

        [HttpGet]
        [Route("results")]
        public OkObjectResult GetResultsAsync()
        {
            return Ok(primeNumbers);
        }

        [HttpPost]
        public void Post([FromBody] PrimeNumbersResult primeNumbersResult)
        {
            primeNumbers.AddRange(primeNumbersResult.Results);
        }
    }
}
