using Client.Forms;
using Client.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        private FileAttachmentService fileAttachmentService;
        private List<Common.Message> chat;
        private bool isForAll;

        public MainForm()
        {
            InitializeComponent();
            client = new ClientService();
            fileAttachmentService = new FileAttachmentService();
            chat = client.GetGlobalChat();
            isForAll = true;
            client.Subscribe(UpdateChatInvoker);
            fileAttachmentService.SubscribeHandler(UpdateFilesList);
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
            client.SendMessage(tbMessage.Text, isForAll, client.ReceiverID, fileAttachmentService.Files.Keys.ToList());

            fileAttachmentService.Initialize();
            tbMessage.Clear();
            lbFiles.Items.Clear();
            bDeleteFile.Enabled = false;
            bGetInfo.Enabled = false;
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
            bUploadFile.Enabled = true;
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
                try
                {
                    chat = client.Conversations[(client.UserID, client.ReceiverID)];
                }
                catch
                {
                    chat = client.GetGlobalChat();
                    rbGlobal.Checked = true;
                }
            }

            foreach (var message in chat)
            {
                lbChat.Items.Add(message.MessageContent);
            }
            lbChat.TopIndex = lbChat.Items.Count - 1;
        }

        private void UpdateServer()
        {
            tbAdress.Text = client.ServerIP.ToString();
            tbPort.Text = client.ServerPort.ToString();
        }

        private void UpdateUsersList()
        {
            cbUsers.Items.Clear();
            lbNotifications.Items.Clear();
            tbID.Text = client.UserID.ToString();
            fileAttachmentService.SetID(client.UserID);

            foreach (var id in client.UserNames.Keys)
            {
                if (id != client.UserID)
                {
                    cbUsers.Items.Add(client.UserNames[id] + "(" + id.ToString() + ")");
                    lbNotifications.Items.Add(client.UserNames[id] + "(" + id.ToString() + ")" + (client.Notifications[id] > 0 ? " - " + client.Notifications[id].ToString() : ""));
                    if (client.UserNames.ContainsKey(client.ReceiverID))
                    {
                        cbUsers.SelectedItem = client.UserNames[client.ReceiverID] + "(" + client.ReceiverID.ToString() + ")";
                    }
                }
            }

            lbNotifications.Items.Add("Global Chat" + (client.Notifications[-1] > 0 ? " - " + client.Notifications[-1].ToString() : ""));
        }

        private void bDisconnect_Click(object sender, EventArgs e)
        {
            isForAll = true;
            client.Stop();

            fileAttachmentService.Initialize();

            tbAdress.Clear();
            tbPort.Clear();
            tbMessage.Clear();
            tbID.Clear();
            lbFiles.Items.Clear();
            tbID.Enabled = false;
            tbUserName.Enabled = false;
            rbPrivate.Enabled = false;
            rbGlobal.Enabled = false;
            tbUserName.ReadOnly = false;
            bFindServer.Enabled = true;
            bSend.Enabled = false;
            bDisconnect.Enabled = false;
            bUploadFile.Enabled = false;
            bViewFiles.Enabled = false;
            bGetInfo.Enabled = false;
            bDeleteFile.Enabled = false;
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
            client.ReceiverID = -1;
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
                client.ReceiverID = IDExtractor.GetIDFromString(cbUsers.SelectedItem.ToString());
                bSend.Enabled = true;
                UpdateChat();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                client.Stop();
            }
            catch
            {
                e.Cancel = false;
            }
        }

        private void tbMessage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                client.SendMessage(tbMessage.Text, isForAll, client.ReceiverID, fileAttachmentService.Files.Keys.ToList());

                fileAttachmentService.Initialize();
                tbMessage.Clear();
                lbFiles.Items.Clear();
                bDeleteFile.Enabled = false;
                bGetInfo.Enabled = false;
            }
        }

        private void tbMessage_Enter(object sender, EventArgs e)
        {
            if (cbUsers.SelectedIndex != -1)
            {
                client.Notifications[IDExtractor.GetIDFromString(cbUsers.SelectedItem.ToString())] = 0;
            }
            else
            {
                client.Notifications[-1] = 0;
            }
            UpdateUsersList();
        }

        private void tbMessage_TextChanged(object sender, EventArgs e)
        {
            if (cbUsers.SelectedIndex != -1)
            {
                client.Notifications[IDExtractor.GetIDFromString(cbUsers.SelectedItem.ToString())] = 0;
            }
            else
            {
                client.Notifications[-1] = 0;
            }
            UpdateUsersList();
        }

        private void lbFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbFiles.SelectedIndex != -1)
            {
                bDeleteFile.Enabled = true;
                bGetInfo.Enabled = true;
            }
            else
            {
                bDeleteFile.Enabled = false;
                bGetInfo.Enabled = false;
            }
        }

        private void UpdateFilesList()
        {
            lbFiles.Items.Clear();

            foreach (var fileID in fileAttachmentService.Files.Keys)
            {
                lbFiles.Items.Add(fileAttachmentService.Files[fileID] + " (" + fileID.ToString() + ")");
            }
        }

        private void bUploadFile_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            string path = openFileDialog.FileName;

            fileAttachmentService.UploadFile(path);
        }

        private void bDeleteFile_Click(object sender, EventArgs e)
        {
            if (lbFiles.SelectedIndex != -1)
            {
                int fileID = IDExtractor.GetIDFromString(lbFiles.SelectedItem.ToString());

                fileAttachmentService.DeleteFile(fileID);
            }
        }

        private async void bGetInfo_Click(object sender, EventArgs e)
        {
            if (lbFiles.SelectedIndex != -1)
            {
                int fileID = IDExtractor.GetIDFromString(lbFiles.SelectedItem.ToString());

                var attributes = await fileAttachmentService.GetFileInfo(fileID);

                MessageBox.Show("File name: " + attributes.Name + "\nFile size: " + attributes.Size.ToString() + " bytes");
            }
        }

        private void lbChat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbChat.SelectedIndex != -1)
            {
                if (chat[lbChat.SelectedIndex].AttachedFiles.Count > 0)
                {
                    bViewFiles.Enabled = true;
                } 
                else
                {
                    bViewFiles.Enabled = false;
                }
            }
        }

        private void bViewFiles_Click(object sender, EventArgs e)
        {
            List<int> files = chat[lbChat.SelectedIndex].AttachedFiles;

            var viewForm = new MessageFilesForm(files);
            viewForm.ShowDialog();
        }
    }
}
