namespace Calcuation.Contracts
{
  using System;
  using System.Collections.Generic;

  public interface IWorkload<T>
  {
    Guid Id { get; }

    IEnumerable<T> GetWorkload();
  }
}
