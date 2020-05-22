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
        private Socket clientSocket;
        public delegate void HandleEvent();
        public event HandleEvent UpdateInterface;
        private Thread thread;
        public List<string> Chat { get; private set; }

        public ClientService()
        {
            Chat = new List<string>();
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

                HandleMessage(sb.ToString());
            }
        }

        private void HandleMessage(string message)
        {
            Chat.Add(message);
            UpdateInterface?.Invoke();
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

        public void Start(IPAddress ip, int port)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var remotePoint = new IPEndPoint(ip, port);
            clientSocket.Connect(remotePoint);
            thread = new Thread(ReceiveMessage);
            thread.Start();
        }
    }
}
