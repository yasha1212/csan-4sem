using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    class ClientService
    {
        private const string broadcastIPAdress = "169.254.70.139";
        private readonly (int, int) GLOBAL_CHAT = (-1, -1);

        private Socket clientSocket;
        private Socket listenerUDP;
        private ISerializer serializeHelper;
        private Thread thread, threadUDP;

        public delegate void HandleEvent();
        public event HandleEvent UpdateInterface;

        public string UserName { get; set; }
        public int ReceiverID { get; set; }
        public int UserID { get; set; }
        public Dictionary<int, string> UserNames { get; private set; }
        public Dictionary<int, int> Notifications { get; set; }
        public Dictionary<(int, int), List<string>> Conversations { get; private set; }
        public int ServerPort { get; private set; }
        public IPAddress ServerIP { get; private set; }

        public ClientService()
        {
            Conversations = new Dictionary<(int, int), List<string>>();
            Conversations[GLOBAL_CHAT] = new List<string>();
            UserNames = new Dictionary<int, string>();
            Notifications = new Dictionary<int, int>();
            Notifications.Add(GLOBAL_CHAT.Item1, 0);
            serializeHelper = new BinarySerializeHelper();
            ReceiverID = -1;
            SetUDPListener();
        }

        private void SetUDPListener()
        {
            listenerUDP = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            listenerUDP.EnableBroadcast = true;
            var localEndPoint = new IPEndPoint(IPAddress.Any, 0);
            listenerUDP.Bind(localEndPoint);
        }

        public void BroadcastRequest()
        {
            var clientInfo = new NodeInfo((listenerUDP.LocalEndPoint as IPEndPoint).Port, NodeInfo.GetIP().ToString());
            var endPoint = new IPEndPoint(IPAddress.Parse(broadcastIPAdress), 8005);
            var searchConnection = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            searchConnection.SendTo(serializeHelper.Serialize(clientInfo), endPoint);
            threadUDP = new Thread(ReceiveServerResponse);
            threadUDP.Start();
        }

        private void ReceiveServerResponse()
        {
            EndPoint endPoint = listenerUDP.LocalEndPoint;

            while(true)
            {
                byte[] data = new byte[1024];
                int bytes = listenerUDP.ReceiveFrom(data, ref endPoint);
                var serverInfo = serializeHelper.Deserialize(data) as NodeInfo;
                ServerIP = IPAddress.Parse(serverInfo.IP);
                ServerPort = serverInfo.Port;
                UpdateInterface?.Invoke();
                threadUDP.Abort();
            }
        }

        public void Subscribe(HandleEvent handler)
        {
            UpdateInterface += handler;
        }

        public void SendMessage(string message, bool isForAll, int receiverID)
        {
            var package = new MessagePackage(message, UserName, receiverID);
            package.IsForAll = isForAll;
            clientSocket.Send(serializeHelper.Serialize(package));
        }

        private void SendMessage(MessagePackage package)
        {
            clientSocket.Send(serializeHelper.Serialize(package));
        }

        private void ReceiveMessage()
        {
            while (clientSocket.Connected)
            {
                var stream = new MemoryStream();

                do
                {
                    var data = new byte[1024];
                    int bytes = clientSocket.Receive(data);
                    stream.Write(data, 0, bytes);
                }
                while (clientSocket.Available > 0);

                object package = serializeHelper.Deserialize(stream.ToArray());
                HandlePackage(package);
            }
        }

        private void HandlePackage(object data)
        {
            if (data as UsersListPackage != null)
            {
                var package = data as UsersListPackage;
                UserNames = package.Users;
                InitializeNotifications();
                UserID = package.UserID;
            }

            if (data as Dictionary<(int, int), List<string>> != null)
            {
                var temp = CloneDictionary(Conversations);
                Conversations = data as Dictionary<(int, int), List<string>>;
                UpdateNotifications(temp);
            }

            UpdateInterface?.Invoke();
        }

        private void UpdateNotifications(Dictionary<(int, int), List<string>> clone)
        {
            foreach (var key in Conversations.Keys)
            {
                if (clone.ContainsKey(key) && key.Item2 != ReceiverID)
                {
                    Notifications[key.Item2] += Conversations[key].Count - clone[key].Count;
                }
            }
        }

        private Dictionary<T, U> CloneDictionary<T, U>(Dictionary<T, U> source)
        {
            var clone = new Dictionary<T, U>();

            foreach (var key in source.Keys)
            {
                clone.Add(key, source[key]);
            }

            return clone;
        }

        private void InitializeNotifications()
        {
            var clone = CloneDictionary(Notifications);

            foreach (var key in clone.Keys)
            {
                if (!UserNames.ContainsKey(key) && key != -1)
                {
                    Notifications.Remove(key);
                }
            }

            foreach (var key in UserNames.Keys)
            {
                if (!Notifications.ContainsKey(key))
                {
                    Notifications.Add(key, 0);
                }
            }
        }

        public void Stop()
        {
            thread?.Abort();
            thread?.Join(100);
            Conversations.Clear();
            Conversations.Add(GLOBAL_CHAT, new List<string>());
            UserNames.Clear();
            clientSocket?.Shutdown(SocketShutdown.Both);
            clientSocket?.Close();
        }

        public void Start()
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var remotePoint = new IPEndPoint(ServerIP, ServerPort);
            clientSocket.Connect(remotePoint);
            SendConnectionMessage();
            thread = new Thread(ReceiveMessage);
            thread.Start();
        }

        private void SendConnectionMessage()
        {
            var package = new MessagePackage(UserName);
            package.IsForConnection = true;
            SendMessage(package);
        }

        public List<String> GetGlobalChat()
        {
            return Conversations[GLOBAL_CHAT];
        }
    }
}
