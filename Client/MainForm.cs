using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class MainForm : Form
    {
        private ClientService client;

        public MainForm()
        {
            InitializeComponent();
            client = new ClientService();
            client.Subscribe(UpdateChatInvoker);
        }

        public void UpdateChatInvoker()
        {
            Action action = UpdateChat;
            Invoke(action);
        }

        private void bSend_Click(object sender, EventArgs e)
        {
            client.SendMessage(tbMessage.Text);
        }

        private void bConnect_Click(object sender, EventArgs e)
        {
            client.Start(IPAddress.Parse("169.254.70.139"), 8005);
        }

        private void UpdateChat()
        {
            lbChat.Items.Clear();

            foreach (var message in client.Chat)
            {
                lbChat.Items.Add(message);
            }
        }

        private void bDisconnect_Click(object sender, EventArgs e)
        {
            client.Stop();
        }
    }
}
