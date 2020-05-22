using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class Connection
    {
        public Socket Client { get; private set; }

        public int ID { get; private set; }

        public Connection(int id, Socket clientSocket)
        {
            ID = id;
            Client = clientSocket;
        }
    }
}
