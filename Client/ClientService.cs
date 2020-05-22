using Common;
using System;
using System.Collections.Generic;
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
        private Socket clientSocket;
        private Socket listenerUDP;
        private ISerializer serializeHelper;
        public delegate void HandleEvent();
        public event HandleEvent UpdateInterface;
        private Thread thread, threadUDP;

        public List<string> Chat { get; private set; }

        public int ServerPort { get; private set; }
        
        public IPAddress ServerIP { get; private set; }

        public ClientService()
        {
            Chat = new List<string>();
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
            clientSocket.Send(Encoding.Unicode.GetBytes(message));
            UpdateInterface?.Invoke();
        }

        private void ReceiveMessage()
        {
            while (clientSocket.Connected)
            {
                var sb = new StringBuilder();

                do
                {
                    var data = new byte[256];
                    int bytes = clientSocket.Receive(data, data.Length, 0);
                    sb.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (clientSocket.Available > 0);

                Chat.Add(sb.ToString());
                UpdateInterface?.Invoke();
            }
        }

        public void Stop()
        {
            thread.Abort();
            thread.Join(100);
            Chat.Clear();
            UpdateInterface?.Invoke();
            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();
        }

        public void Start()
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var remotePoint = new IPEndPoint(ServerIP, ServerPort);
            clientSocket.Connect(remotePoint);
            thread = new Thread(ReceiveMessage);
            thread.Start();
        }
    }
}
