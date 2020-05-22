using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Common;

namespace Server
{
    class ServerService
    {
        private const int MAX_CONNECTIONS_AMOUNT = 10;
        private Socket listenSocket;
        private Socket clientSocket;
        private int id;
        private ISerializer serializeHelper;

        public Dictionary<int, Socket> Connections { get; private set; }

        public Dictionary<int, string> UserNames { get; private set; }

        public int Port { get; private set; }

        public ServerService(int port)
        {
            Port = port;
            id = 0;
            Connections = new Dictionary<int, Socket>();
            UserNames = new Dictionary<int, string>();
            serializeHelper = new BinarySerializeHelper();
        }

        public void Stop()
        {
            listenSocket.Close();
        }

        public void Start()
        {
            var endPoint = new IPEndPoint(NodeInfo.GetIP(), Port);
            Console.WriteLine(NodeInfo.GetIP().ToString());
            listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listenSocket.Bind(endPoint);
            listenSocket.Listen(MAX_CONNECTIONS_AMOUNT);
            var threadUDP = new Thread(ListenBroadcast);
            threadUDP.Start();
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
                MessagePackage package = null;

                do
                {
                    try
                    {
                        byte[] data = new byte[1024];
                        int bytes = clientInfo.Client.Receive(data);
                        package = serializeHelper.Deserialize(data) as MessagePackage;

                        if (package.IsForConnection)
                        {
                            ConnectNameAndID(clientInfo.ID, package.SenderName);
                            Console.WriteLine("Пользователь соединения " + clientInfo.ID.ToString() + " теперь известен как " + package.SenderName);
                            SendMessage("К вам присоединился пользователь " + package.SenderName + ". Добро пожаловать!");  
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Соединение " + clientInfo.ID.ToString() + " прервано.");
                        Connections.Remove(clientInfo.ID);
                    }
                }
                while (clientInfo.Client.Available > 0);

                if(package?.Message?.Length > 0)
                {
                    SendMessage("[" + DateTime.Now.ToShortTimeString() + "] " + UserNames[clientInfo.ID] + ": " + package.Message);
                }
            }
        }

        private void ConnectNameAndID(int id, string name)
        {
            if(HasNoName(id))
            {
                UserNames.Add(id, name);
            }
            else
            {
                UserNames[id] = name;
            }
        }

        private bool HasNoName(int id)
        {
            if(UserNames.ContainsKey(id))
            {
                return false;
            }
            return true;
        }

        private void ListenBroadcast()
        {
            var listener = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            listener.EnableBroadcast = true;
            var localEndPoint = new IPEndPoint(IPAddress.Any, Port);
            listener.Bind(localEndPoint);
            EndPoint endPoint = localEndPoint;

            while(true)
            {
                byte[] data = new byte[1024];
                int bytes = listener.ReceiveFrom(data, ref endPoint);
                NodeInfo clientInfo = serializeHelper.Deserialize(data) as NodeInfo;
                HandleClientSearchRequest(clientInfo);
            }
        }

        private void HandleClientSearchRequest(NodeInfo clientInfo)
        {
            var serverInfo = new NodeInfo(Port, NodeInfo.GetIP().ToString());
            var clientConnection = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            var clientEndPoint = new IPEndPoint(IPAddress.Parse(clientInfo.IP), clientInfo.Port);
            clientConnection.SendTo(serializeHelper.Serialize(serverInfo), clientEndPoint);
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
