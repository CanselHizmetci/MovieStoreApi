using System;
namespace MovieStoreApi.Services
{
    public class ConsoleLogger : ILoggerService
    {

        public void write(string message)
        {
            Console.WriteLine("[ConsoleLogger] - " + message);
        }
    }
}

