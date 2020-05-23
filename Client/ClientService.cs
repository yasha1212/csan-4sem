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
        public delegate void HandleEvent();
        public event HandleEvent UpdateInterface;
        private Thread thread, threadUDP;

        public string UserName { get; set; }

        public int UserID { get; set; }

        public Dictionary<int, string> UserNames { get; private set; }

        public Dictionary<(int, int), List<string>> Conversations { get; private set; }

        public int ServerPort { get; private set; }
        
        public IPAddress ServerIP { get; private set; }

        public ClientService()
        {
            Conversations = new Dictionary<(int, int), List<string>>();
            Conversations[GLOBAL_CHAT] = new List<string>();
            UserNames = new Dictionary<int, string>();
            serializeHelper = new BinarySerializeHelper();
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

        public void SendMessage(string message)
        {
            var package = new MessagePackage(message, UserName);
            package.IsForAll = true;
            clientSocket.Send(serializeHelper.Serialize(package));
            UpdateInterface?.Invoke();
        }

        private void SendMessage(MessagePackage package)
        {
            clientSocket.Send(serializeHelper.Serialize(package));
            UpdateInterface?.Invoke();
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
                UserID = package.UserID;
            }

            if (data as Dictionary<(int, int), List<string>> != null)
            {
                Conversations = data as Dictionary<(int, int), List<string>>;
            }

            UpdateInterface?.Invoke();
        }

        public void Stop()
        {
            SendDisconnectionMessage();
            thread.Abort();
            thread.Join(100);
            Conversations.Clear();
            Conversations.Add(GLOBAL_CHAT, new List<string>());
            UserNames.Clear();
            UpdateInterface?.Invoke();
            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();
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

        private void SendDisconnectionMessage()
        {
            var package = new MessagePackage(UserName);
            package.IsForDisconnection = true;
            SendMessage(package);
        }

        public List<String> GetGlobalChat()
        {
            return Conversations[GLOBAL_CHAT];
        }
    }
}
