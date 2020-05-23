using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Common;

namespace Server
{
    enum ServerMessageType
    {
        ClientConnection,
        ClientDisconnection
    }

    class ServerService
    {
        private const int MAX_CONNECTIONS_AMOUNT = 10;
        private readonly (int, int) GLOBAL_CHAT = (-1, -1);
        private Socket listenSocket;
        private Socket clientSocket;
        private int id;
        private ISerializer serializeHelper;

        public Dictionary<int, Socket> Connections { get; private set; }

        public Dictionary<(int, int), List<string>> Conversations { get; private set; }

        public Dictionary<int, string> UserNames { get; private set; }

        public int Port { get; private set; }

        public ServerService(int port)
        {
            Port = port;
            id = 0;
            Connections = new Dictionary<int, Socket>();
            Conversations = new Dictionary<(int, int), List<string>>();
            Conversations[GLOBAL_CHAT] = new List<string>();
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
                        HandlePackage(package, clientInfo);
                    }
                    catch
                    {
                        Console.WriteLine("Соединение " + clientInfo.ID.ToString() + " прервано.");
                        RemoveClient(UserNames[clientInfo.ID], clientInfo.ID);
                    }
                }
                while (clientInfo.Client.Available > 0);

                if(package?.Message?.Length > 0)
                {
                    SendMessage(package, clientInfo.ID);
                    //SendMessage("[" + DateTime.Now.ToShortDateString() + ", " + DateTime.Now.ToShortTimeString() + "] " + UserNames[clientInfo.ID] + ": " + package.Message);
                }
            }

            clientInfo.Client.Close();
        }

        private void SendMessage(MessagePackage package, int senderID)
        {
            var message = "[" + DateTime.Now.ToShortDateString() + ", " + DateTime.Now.ToShortTimeString() + "] " + UserNames[senderID] + ": " + package.Message;

            if (package.IsForAll)
            {
                Conversations[GLOBAL_CHAT].Add(message);

                /*foreach (var clientID in Connections.Keys)
                {
                    Connections[clientID].Send(serializeHelper.Serialize(package.Message));
                    Conversations[(senderID, clientID)].Add(package.Message);
                } */
            }
            else
            {
                Conversations[(senderID, package.ReceiverID)].Add(message);
                Conversations[(package.ReceiverID, senderID)].Add(message);
            }

            NotifyClients();
        }

        private void HandlePackage(MessagePackage package, Connection connectionInfo)
        {
            if (package.IsForConnection)
            {
                AddClient(package.SenderName, connectionInfo.ID);
            }

            if (package.IsForDisconnection)
            {
                RemoveClient(package.SenderName, connectionInfo.ID);
            }
        }

        private void NotifyClients()
        {
            foreach (var client in Connections.Values)
            {
                client.Send(serializeHelper.Serialize(Conversations));
            }
        }

        private void SendServerMessage(ServerMessageType type, string userName)
        {
            if(type == ServerMessageType.ClientConnection)
            {
                Conversations[GLOBAL_CHAT].Add("К вам присоединился пользователь " + userName + ". Добро пожаловать!");
            }
            if(type == ServerMessageType.ClientDisconnection)
            {
                Conversations[GLOBAL_CHAT].Add("Пользователь " + userName + " вышел из чата.");
                foreach(var conversation in Conversations.Values)
                {
                    conversation.Add("Пользователь " + userName + " вышел из чата.");
                }
            }

            NotifyClients();
        }

        private void RemoveClient(string userName, int id)
        {
            UserNames.Remove(id);
            SendUsersList();
            Console.WriteLine("Пользователь " + id.ToString() + " (" + userName + ") отсоединился");

            foreach(var clientID in Connections.Keys)
            {
                Conversations.Remove((clientID, id));
                Conversations.Remove((id, clientID));
            }

            SendServerMessage(ServerMessageType.ClientDisconnection, userName);
            Connections[id].Shutdown(SocketShutdown.Both);
            Connections.Remove(id);
            DisplayCurrentUsers();
        }

        private void AddClient(string userName, int id)
        {
            ConnectNameAndID(id, userName);
            Console.WriteLine("Пользователь соединения " + id.ToString() + " теперь известен как " + userName);
            SendUsersList();
            
            foreach(var clientID in Connections.Keys)
            {
                if (clientID != id)
                {
                    Conversations.Add((id, clientID), new List<string>());
                    Conversations.Add((clientID, id), new List<string>());
                }
            }

            SendServerMessage(ServerMessageType.ClientConnection, userName);
            DisplayCurrentUsers();
        }

        private void DisplayCurrentUsers()
        {
            Console.WriteLine();
            Console.WriteLine("--------------------------------Active-----------------------------------");
            foreach (var userID in UserNames.Keys)
            {
                Console.WriteLine(userID.ToString() + " - " + UserNames[userID]);
            }
            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine();
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

        private void SendUsersList()
        {
            foreach(var clientID in Connections.Keys)
            {
                var package = new UsersListPackage(clientID, UserNames);
                Connections[clientID].Send(serializeHelper.Serialize(package));
            }
        }
    }
}
