using System;
using System.Net;

namespace Common
{
    [Serializable]
    public class NodeInfo
    {
        public int Port { get; private set; }

        public string IP { get; private set; }

        public NodeInfo(int port, string ip)
        {
            Port = port;
            IP = ip;
        }

        public static IPAddress GetIP()
        {
            var adresses = Dns.GetHostAddresses(Dns.GetHostName());
            IPAddress currentAdress = null;

            foreach (var adress in adresses)
            {
                if (adress.GetAddressBytes().Length == 4)
                {
                    currentAdress = adress;
                    break;
                }
            }

            return currentAdress;
        }
    }
}
