using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server
{
    class ServerService
    {
        private const int MAX_CONNECTIONS_AMOUNT = 10;
        private Socket listenSocket;
        private Socket clientSocket;
        private int id;

        public Dictionary<int, Socket> Connections { get; private set; }

        public int Port { get; private set; }

        private IPAddress GetIP()
        {
            var adresses = Dns.GetHostAddresses(Dns.GetHostName());
            IPAddress currentAdress = null;

            foreach(var adress in adresses)
            {
                if(adress.GetAddressBytes().Length == 4)
                {
                    currentAdress = adress;
                    break;
                }
            }

            return currentAdress;
        }

        public ServerService(int port)
        {
            Port = port;
            id = 0;
            Connections = new Dictionary<int, Socket>();
        }

        public void Stop()
        {
            listenSocket.Close();
        }

        public void Start()
        {
            var endPoint = new IPEndPoint(GetIP(), Port);
            Console.WriteLine(GetIP().ToString());
            listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listenSocket.Bind(endPoint);
            listenSocket.Listen(MAX_CONNECTIONS_AMOUNT);
            Console.WriteLine("Сервер запущен. Ожидание подключений...");

            while(true)
            {
                clientSocket = listenSocket.Accept();
                Connections.Add(id, clientSocket);
                var thread = new Thread(new ParameterizedThreadStart(ReceiveData));
                thread.Start(new Connection(id++, clientSocket));
            }
        }

        public void ReceiveData(object connection)
        {
            var clientInfo = connection as Connection;
            Console.WriteLine("Соединение " + clientInfo.ID.ToString() + " установлено.");

            while(clientInfo.Client.Connected)
            {
                var sb = new StringBuilder();
                int bytes = 0;
                byte[] data = new byte[256];

                do
                {
                    try
                    {
                        bytes = clientInfo.Client.Receive(data);
                        sb.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    catch
                    {
                        Console.WriteLine("Соединение " + clientInfo.ID.ToString() + " прервано.");
                        Connections.Remove(clientInfo.ID);
                    }
                }
                while (clientInfo.Client.Available > 0);

                if(sb.ToString().Length > 0)
                {
                    SendMessage(clientInfo.ID.ToString() + ": " + sb.ToString() + " - " + DateTime.Now.ToShortTimeString());
                }
            }
        }

        public void SendMessage(string message)
        {
            foreach(var client in Connections.Values)
            {
                client.Send(Encoding.Unicode.GetBytes(message));
            }
        }
    }
}
