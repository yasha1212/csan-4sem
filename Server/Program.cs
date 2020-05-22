using System;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new ServerService(8005);
            server.Start();
        }
    }
}
