﻿using System;
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
        private List<string> chat;
        private int receiverID;
        private bool isForAll;

        public MainForm()
        {
            InitializeComponent();
            client = new ClientService();
            chat = client.GetGlobalChat();
            isForAll = true;
            client.Subscribe(UpdateChatInvoker);
        }

        public void UpdateChatInvoker()
        {
            Action action = UpdateChat;
            action += UpdateServer;
            action += UpdateUsersList;
            Invoke(action);
        }

        private void bSend_Click(object sender, EventArgs e)
        {
            client.SendMessage(tbMessage.Text, isForAll, receiverID);

            tbMessage.Clear();
        }

        private void bConnect_Click(object sender, EventArgs e)
        {
            client.Start();

            tbMessage.Enabled = true;
            tbID.Enabled = true;
            bSend.Enabled = true;
            bDisconnect.Enabled = true;
            rbGlobal.Enabled = true;
            rbPrivate.Enabled = true;
            bConnect.Enabled = false;
            bAcceptName.Enabled = false;
            tbUserName.ReadOnly = true;
        }

        private void UpdateChat()
        {
            lbChat.Items.Clear();

            if(isForAll)
            {
                chat = client.GetGlobalChat();
            }
            else
            {
                receiverID = IDParser.GetIDFromString(cbUsers.SelectedItem.ToString());
                chat = client.Conversations[(client.UserID, receiverID)];
            }

            foreach (var message in chat)
            {
                lbChat.Items.Add(message);
            }
        }

        private void UpdateServer()
        {
            tbAdress.Text = client.ServerIP.ToString();
            tbPort.Text = client.ServerPort.ToString();
        }

        private void UpdateUsersList()
        {
            if (isForAll)
            {
                cbUsers.Items.Clear();
                tbID.Text = client.UserID.ToString();

                foreach (var id in client.UserNames.Keys)
                {
                    if (id != client.UserID)
                    {
                        cbUsers.Items.Add(client.UserNames[id] + "(" + id.ToString() + ")");
                    }
                }
            }
        }

        private void bDisconnect_Click(object sender, EventArgs e)
        {
            client.Stop();

            tbAdress.Clear();
            tbPort.Clear();
            tbMessage.Clear();
            tbID.Clear();
            tbID.Enabled = false;
            tbUserName.Enabled = false;
            rbPrivate.Enabled = false;
            rbGlobal.Enabled = false;
            tbUserName.ReadOnly = false;
            bFindServer.Enabled = true;
            bSend.Enabled = false;
            bDisconnect.Enabled = false;
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

        private void rbGlobal_CheckedChanged(object sender, EventArgs e)
        {
            cbUsers.Enabled = false;
            bSend.Enabled = true;
            isForAll = true;
            UpdateChat();
        }

        private void rbPrivate_CheckedChanged(object sender, EventArgs e)
        {
            cbUsers.Enabled = true;
            bSend.Enabled = false;
            isForAll = false;
        }

        private void cbUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbUsers.SelectedIndex != -1)
            {
                bSend.Enabled = true;
                UpdateChat();
            }
        }
    }
}
