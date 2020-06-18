using System;

namespace FileStorage
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new StorageServer();
            server.Start();
        }
    }
}
