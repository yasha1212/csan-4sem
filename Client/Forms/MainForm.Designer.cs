namespace Client
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbMessage = new System.Windows.Forms.TextBox();
            this.bSend = new System.Windows.Forms.Button();
            this.bConnect = new System.Windows.Forms.Button();
            this.bDisconnect = new System.Windows.Forms.Button();
            this.lbChat = new System.Windows.Forms.ListBox();
            this.tbAdress = new System.Windows.Forms.TextBox();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.bFindServer = new System.Windows.Forms.Button();
            this.tbUserName = new System.Windows.Forms.TextBox();
            this.laUserName = new System.Windows.Forms.Label();
            this.bAcceptName = new System.Windows.Forms.Button();
            this.cbUsers = new System.Windows.Forms.ComboBox();
            this.tbID = new System.Windows.Forms.TextBox();
            this.laUserID = new System.Windows.Forms.Label();
            this.rbGlobal = new System.Windows.Forms.RadioButton();
            this.rbPrivate = new System.Windows.Forms.RadioButton();
            this.lbNotifications = new System.Windows.Forms.ListBox();
            this.laNotifications = new System.Windows.Forms.Label();
            this.lbFiles = new System.Windows.Forms.ListBox();
            this.bGetInfo = new System.Windows.Forms.Button();
            this.bDeleteFile = new System.Windows.Forms.Button();
            this.bViewFiles = new System.Windows.Forms.Button();
            this.bUploadFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbMessage
            // 
            this.tbMessage.Enabled = false;
            this.tbMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbMessage.Location = new System.Drawing.Point(12, 466);
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.Size = new System.Drawing.Size(660, 28);
            this.tbMessage.TabIndex = 0;
            this.tbMessage.TextChanged += new System.EventHandler(this.tbMessage_TextChanged);
            this.tbMessage.Enter += new System.EventHandler(this.tbMessage_Enter);
            this.tbMessage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbMessage_KeyPress);
            // 
            // bSend
            // 
            this.bSend.Enabled = false;
            this.bSend.Location = new System.Drawing.Point(678, 466);
            this.bSend.Name = "bSend";
            this.bSend.Size = new System.Drawing.Size(123, 29);
            this.bSend.TabIndex = 1;
            this.bSend.Text = "Send";
            this.bSend.UseVisualStyleBackColor = true;
            this.bSend.Click += new System.EventHandler(this.bSend_Click);
            // 
            // bConnect
            // 
            this.bConnect.Enabled = false;
            this.bConnect.Location = new System.Drawing.Point(807, 86);
            this.bConnect.Name = "bConnect";
            this.bConnect.Size = new System.Drawing.Size(149, 32);
            this.bConnect.TabIndex = 2;
            this.bConnect.Text = "Connect";
            this.bConnect.UseVisualStyleBackColor = true;
            this.bConnect.Click += new System.EventHandler(this.bConnect_Click);
            // 
            // bDisconnect
            // 
            this.bDisconnect.Enabled = false;
            this.bDisconnect.Location = new System.Drawing.Point(962, 86);
            this.bDisconnect.Name = "bDisconnect";
            this.bDisconnect.Size = new System.Drawing.Size(149, 32);
            this.bDisconnect.TabIndex = 3;
            this.bDisconnect.Text = "Disconnect";
            this.bDisconnect.UseVisualStyleBackColor = true;
            this.bDisconnect.Click += new System.EventHandler(this.bDisconnect_Click);
            // 
            // lbChat
            // 
            this.lbChat.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbChat.FormattingEnabled = true;
            this.lbChat.HorizontalScrollbar = true;
            this.lbChat.ItemHeight = 20;
            this.lbChat.Location = new System.Drawing.Point(12, 12);
            this.lbChat.Name = "lbChat";
            this.lbChat.Size = new System.Drawing.Size(789, 444);
            this.lbChat.TabIndex = 4;
            this.lbChat.SelectedIndexChanged += new System.EventHandler(this.lbChat_SelectedIndexChanged);
            // 
            // tbAdress
            // 
            this.tbAdress.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbAdress.Location = new System.Drawing.Point(807, 52);
            this.tbAdress.Name = "tbAdress";
            this.tbAdress.ReadOnly = true;
            this.tbAdress.Size = new System.Drawing.Size(194, 28);
            this.tbAdress.TabIndex = 5;
            // 
            // tbPort
            // 
            this.tbPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbPort.Location = new System.Drawing.Point(1007, 52);
            this.tbPort.Name = "tbPort";
            this.tbPort.ReadOnly = true;
            this.tbPort.Size = new System.Drawing.Size(104, 28);
            this.tbPort.TabIndex = 6;
            // 
            // bFindServer
            // 
            this.bFindServer.Location = new System.Drawing.Point(807, 12);
            this.bFindServer.Name = "bFindServer";
            this.bFindServer.Size = new System.Drawing.Size(304, 34);
            this.bFindServer.TabIndex = 7;
            this.bFindServer.Text = "Find Server";
            this.bFindServer.UseVisualStyleBackColor = true;
            this.bFindServer.Click += new System.EventHandler(this.bFindServer_Click);
            // 
            // tbUserName
            // 
            this.tbUserName.Enabled = false;
            this.tbUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbUserName.Location = new System.Drawing.Point(807, 177);
            this.tbUserName.Name = "tbUserName";
            this.tbUserName.Size = new System.Drawing.Size(194, 28);
            this.tbUserName.TabIndex = 8;
            // 
            // laUserName
            // 
            this.laUserName.AutoSize = true;
            this.laUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laUserName.Location = new System.Drawing.Point(807, 154);
            this.laUserName.Name = "laUserName";
            this.laUserName.Size = new System.Drawing.Size(94, 20);
            this.laUserName.TabIndex = 9;
            this.laUserName.Text = "User Name";
            // 
            // bAcceptName
            // 
            this.bAcceptName.Enabled = false;
            this.bAcceptName.Location = new System.Drawing.Point(807, 211);
            this.bAcceptName.Name = "bAcceptName";
            this.bAcceptName.Size = new System.Drawing.Size(304, 32);
            this.bAcceptName.TabIndex = 10;
            this.bAcceptName.Text = "Accept";
            this.bAcceptName.UseVisualStyleBackColor = true;
            this.bAcceptName.Click += new System.EventHandler(this.bAcceptName_Click);
            // 
            // cbUsers
            // 
            this.cbUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUsers.Enabled = false;
            this.cbUsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbUsers.FormattingEnabled = true;
            this.cbUsers.Location = new System.Drawing.Point(807, 466);
            this.cbUsers.Name = "cbUsers";
            this.cbUsers.Size = new System.Drawing.Size(304, 28);
            this.cbUsers.TabIndex = 11;
            this.cbUsers.SelectedIndexChanged += new System.EventHandler(this.cbUsers_SelectedIndexChanged);
            // 
            // tbID
            // 
            this.tbID.Enabled = false;
            this.tbID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbID.Location = new System.Drawing.Point(1007, 177);
            this.tbID.Name = "tbID";
            this.tbID.ReadOnly = true;
            this.tbID.Size = new System.Drawing.Size(104, 28);
            this.tbID.TabIndex = 12;
            // 
            // laUserID
            // 
            this.laUserID.AutoSize = true;
            this.laUserID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laUserID.Location = new System.Drawing.Point(1003, 154);
            this.laUserID.Name = "laUserID";
            this.laUserID.Size = new System.Drawing.Size(67, 20);
            this.laUserID.TabIndex = 13;
            this.laUserID.Text = "User ID";
            // 
            // rbGlobal
            // 
            this.rbGlobal.AutoSize = true;
            this.rbGlobal.Checked = true;
            this.rbGlobal.Enabled = false;
            this.rbGlobal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbGlobal.Location = new System.Drawing.Point(807, 258);
            this.rbGlobal.Name = "rbGlobal";
            this.rbGlobal.Size = new System.Drawing.Size(128, 28);
            this.rbGlobal.TabIndex = 14;
            this.rbGlobal.TabStop = true;
            this.rbGlobal.Text = "Global Chat";
            this.rbGlobal.UseVisualStyleBackColor = true;
            this.rbGlobal.CheckedChanged += new System.EventHandler(this.rbGlobal_CheckedChanged);
            // 
            // rbPrivate
            // 
            this.rbPrivate.AutoSize = true;
            this.rbPrivate.Enabled = false;
            this.rbPrivate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbPrivate.Location = new System.Drawing.Point(981, 258);
            this.rbPrivate.Name = "rbPrivate";
            this.rbPrivate.Size = new System.Drawing.Size(130, 28);
            this.rbPrivate.TabIndex = 15;
            this.rbPrivate.Text = "Private Chat";
            this.rbPrivate.UseVisualStyleBackColor = true;
            this.rbPrivate.CheckedChanged += new System.EventHandler(this.rbPrivate_CheckedChanged);
            // 
            // lbNotifications
            // 
            this.lbNotifications.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbNotifications.FormattingEnabled = true;
            this.lbNotifications.IntegralHeight = false;
            this.lbNotifications.ItemHeight = 20;
            this.lbNotifications.Location = new System.Drawing.Point(807, 332);
            this.lbNotifications.Name = "lbNotifications";
            this.lbNotifications.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lbNotifications.Size = new System.Drawing.Size(304, 124);
            this.lbNotifications.TabIndex = 16;
            // 
            // laNotifications
            // 
            this.laNotifications.AutoSize = true;
            this.laNotifications.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laNotifications.Location = new System.Drawing.Point(807, 309);
            this.laNotifications.Name = "laNotifications";
            this.laNotifications.Size = new System.Drawing.Size(102, 20);
            this.laNotifications.TabIndex = 17;
            this.laNotifications.Text = "Notifications";
            // 
            // lbFiles
            // 
            this.lbFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbFiles.FormattingEnabled = true;
            this.lbFiles.IntegralHeight = false;
            this.lbFiles.ItemHeight = 20;
            this.lbFiles.Location = new System.Drawing.Point(807, 540);
            this.lbFiles.Name = "lbFiles";
            this.lbFiles.Size = new System.Drawing.Size(304, 124);
            this.lbFiles.TabIndex = 18;
            this.lbFiles.SelectedIndexChanged += new System.EventHandler(this.lbFiles_SelectedIndexChanged);
            // 
            // bGetInfo
            // 
            this.bGetInfo.Enabled = false;
            this.bGetInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bGetInfo.Location = new System.Drawing.Point(442, 616);
            this.bGetInfo.Name = "bGetInfo";
            this.bGetInfo.Size = new System.Drawing.Size(359, 48);
            this.bGetInfo.TabIndex = 22;
            this.bGetInfo.Text = "Get file info";
            this.bGetInfo.UseVisualStyleBackColor = true;
            this.bGetInfo.Click += new System.EventHandler(this.bGetInfo_Click);
            // 
            // bDeleteFile
            // 
            this.bDeleteFile.Enabled = false;
            this.bDeleteFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bDeleteFile.Location = new System.Drawing.Point(442, 540);
            this.bDeleteFile.Name = "bDeleteFile";
            this.bDeleteFile.Size = new System.Drawing.Size(359, 48);
            this.bDeleteFile.TabIndex = 23;
            this.bDeleteFile.Text = "Delete file";
            this.bDeleteFile.UseVisualStyleBackColor = true;
            this.bDeleteFile.Click += new System.EventHandler(this.bDeleteFile_Click);
            // 
            // bViewFiles
            // 
            this.bViewFiles.Enabled = false;
            this.bViewFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bViewFiles.Location = new System.Drawing.Point(12, 616);
            this.bViewFiles.Name = "bViewFiles";
            this.bViewFiles.Size = new System.Drawing.Size(359, 48);
            this.bViewFiles.TabIndex = 24;
            this.bViewFiles.Text = "View files";
            this.bViewFiles.UseVisualStyleBackColor = true;
            this.bViewFiles.Click += new System.EventHandler(this.bViewFiles_Click);
            // 
            // bUploadFile
            // 
            this.bUploadFile.Enabled = false;
            this.bUploadFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bUploadFile.Location = new System.Drawing.Point(12, 540);
            this.bUploadFile.Name = "bUploadFile";
            this.bUploadFile.Size = new System.Drawing.Size(359, 48);
            this.bUploadFile.TabIndex = 25;
            this.bUploadFile.Text = "Upload file";
            this.bUploadFile.UseVisualStyleBackColor = true;
            this.bUploadFile.Click += new System.EventHandler(this.bUploadFile_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1123, 676);
            this.Controls.Add(this.bUploadFile);
            this.Controls.Add(this.bViewFiles);
            this.Controls.Add(this.bDeleteFile);
            this.Controls.Add(this.bGetInfo);
            this.Controls.Add(this.lbFiles);
            this.Controls.Add(this.laNotifications);
            this.Controls.Add(this.lbNotifications);
            this.Controls.Add(this.rbPrivate);
            this.Controls.Add(this.rbGlobal);
            this.Controls.Add(this.laUserID);
            this.Controls.Add(this.tbID);
            this.Controls.Add(this.cbUsers);
            this.Controls.Add(this.bAcceptName);
            this.Controls.Add(this.laUserName);
            this.Controls.Add(this.tbUserName);
            this.Controls.Add(this.bFindServer);
            this.Controls.Add(this.tbPort);
            this.Controls.Add(this.tbAdress);
            this.Controls.Add(this.lbChat);
            this.Controls.Add(this.bDisconnect);
            this.Controls.Add(this.bConnect);
            this.Controls.Add(this.bSend);
            this.Controls.Add(this.tbMessage);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbMessage;
        private System.Windows.Forms.Button bSend;
        private System.Windows.Forms.Button bConnect;
        private System.Windows.Forms.Button bDisconnect;
        private System.Windows.Forms.ListBox lbChat;
        private System.Windows.Forms.TextBox tbAdress;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.Button bFindServer;
        private System.Windows.Forms.TextBox tbUserName;
        private System.Windows.Forms.Label laUserName;
        private System.Windows.Forms.Button bAcceptName;
        private System.Windows.Forms.ComboBox cbUsers;
        private System.Windows.Forms.TextBox tbID;
        private System.Windows.Forms.Label laUserID;
        private System.Windows.Forms.RadioButton rbGlobal;
        private System.Windows.Forms.RadioButton rbPrivate;
        private System.Windows.Forms.ListBox lbNotifications;
        private System.Windows.Forms.Label laNotifications;
        private System.Windows.Forms.ListBox lbFiles;
        private System.Windows.Forms.Button bGetInfo;
        private System.Windows.Forms.Button bDeleteFile;
        private System.Windows.Forms.Button bViewFiles;
        private System.Windows.Forms.Button bUploadFile;
    }
}

