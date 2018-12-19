using System;

namespace AsyncCorrelationLogging.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Logger logger = new Logger();
            logger.log("Test");
        }
    }
}