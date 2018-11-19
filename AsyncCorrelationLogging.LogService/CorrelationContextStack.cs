namespace AsyncCorrelationLogging.LogService
{
  using System;
  using System.Collections.Immutable;
  using System.Linq;
  using System.Runtime.CompilerServices;
  using System.Runtime.Remoting.Messaging;

  /// <summary>
  /// Based on: http://blog.stephencleary.com/2013/04/implicit-async-context-asynclocal.html 
  /// </summary>
  internal static class CorrelationContextStack
  {
    private static readonly string correlationIdStackName = "CorrelationIdStack";

    public static bool IsInCorrelation
    {
      get
      {
        return !CurrentContext.IsEmpty;
      }
    }

    public static bool IsEmpty
    {
      get
      {
        return CurrentContext.IsEmpty;
      }
    }

    private sealed class Wrapper : MarshalByRefObject
    {
      public ImmutableStack<string> Value { get; set; }
    }

    private static ImmutableStack<string> CurrentContext
    {
      get
      {
        var ret = CallContext.LogicalGetData(correlationIdStackName) as Wrapper;

        return ret == null 
          ? ImmutableStack.Create<string>() 
          : ret.Value;
      }

      set
      {
        CallContext.LogicalSetData(correlationIdStackName, new Wrapper { Value = value });
      }
    }

    public static IDisposable Push([CallerMemberName] string context = "")
    {
      CurrentContext = CurrentContext.Push(context);
      return new PopWhenDisposed();
    }

    private static void Pop()
    {
      CurrentContext = CurrentContext.Pop();
    }

    private sealed class PopWhenDisposed : IDisposable
    {
      private bool disposed;

      public void Dispose()
      {
        if (disposed)
        {
          return;
        }

        Pop();
        disposed = true;
      }
    }

    public static string CurrentCorrelationIdStack
    {
      get
      {
        return string.Join("-", CurrentContext.Reverse());
      }
    }

    public static string CurrentCorrelationId
    {
      get
      {
        return CurrentContext.Peek();
      }
    }
  }
}