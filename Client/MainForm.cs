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
            action += UpdateServer;
            Invoke(action);
        }

        private void bSend_Click(object sender, EventArgs e)
        {
            client.SendMessage(tbMessage.Text);

            tbMessage.Clear();
        }

        private void bConnect_Click(object sender, EventArgs e)
        {
            client.Start();

            tbMessage.Enabled = true;
            bSend.Enabled = true;
            bDisconnect.Enabled = true;
            bConnect.Enabled = false;
            bAcceptName.Enabled = false;
            tbUserName.Enabled = false;
        }

        private void UpdateChat()
        {
            lbChat.Items.Clear();

            foreach (var message in client.Chat)
            {
                lbChat.Items.Add(message);
            }
        }

        private void UpdateServer()
        {
            tbAdress.Text = client.ServerIP.ToString();
            tbPort.Text = client.ServerPort.ToString();
        }

        private void bDisconnect_Click(object sender, EventArgs e)
        {
            client.Stop();

            tbAdress.Clear();
            tbPort.Clear();
            tbMessage.Clear();
            bFindServer.Enabled = true;
            bSend.Enabled = false;
            tbMessage.Enabled = false;
        }

        private void bFindServer_Click(object sender, EventArgs e)
        {
            client.BroadcastRequest();

            tbUserName.Enabled = true;
            bFindServer.Enabled = false;
            bAcceptName.Enabled = true;
        }

        private void bAcceptName_Click(object sender, EventArgs e)
        {
            if(tbUserName.Text.Length > 0)
            {
                client.UserName = tbUserName.Text;

                bConnect.Enabled = true;
            }
        }
    }
}
