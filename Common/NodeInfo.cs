using System;
using System.Net;

namespace Common
{
    [Serializable]
    public class NodeInfo
    {
        public int Port { get; private set; }

        public IPAddress IP { get; private set; }

        public NodeInfo(int port, IPAddress ip)
        {
            Port = port;
            IP = ip;
        }
    }
}
