namespace AsyncCorrelationLogging
{
    using System;


    public class Logger : IDisposable
    {
        private IDisposable rootCorrelation;

        public bool IsInCorrelation()
        {
            return CorrelationContextStack.IsInCorrelation;
        }

        public IDisposable Correlate(string id = null)
        {
            string actualId = id ?? Guid.NewGuid().ToString("N");
            return CorrelationContextStack.Push(actualId);
        }

        public void Log(string message)
        {
            if (!IsInCorrelation())
            {
                rootCorrelation = Correlate();
            }

            System.Diagnostics.Trace.WriteLine($"({CorrelationContextStack.CurrentCorrelationIdStack}) {message}");
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            rootCorrelation?.Dispose();
        }
    }
}