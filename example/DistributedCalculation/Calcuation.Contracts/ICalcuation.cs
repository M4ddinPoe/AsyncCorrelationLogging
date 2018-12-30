namespace Calcuation.Contracts
{
  using System.ComponentModel;

  public interface ICalcuation<TWorkload, TResult>
  {
    TResult Calculate(TWorkload workload);
  }
}
