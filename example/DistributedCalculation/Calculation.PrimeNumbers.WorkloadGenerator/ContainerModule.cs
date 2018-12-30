using Autofac;

namespace Calculation.PrimeNumbers.WorkloadGenerator
{
    public class ContainerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<PrimeNumberGenerator>().SingleInstance();
        }
    }
}
