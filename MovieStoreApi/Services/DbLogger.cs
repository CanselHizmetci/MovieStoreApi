using System;
namespace MovieStoreApi.Services
{
    public class DbLogger : ILoggerService
    {
        public void write(string message)
        {
            Console.WriteLine("[DbLogger] - " + message);
        }
    }
}

