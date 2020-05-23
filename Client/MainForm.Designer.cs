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
            this.SuspendLayout();
            // 
            // tbMessage
            // 
            this.tbMessage.Enabled = false;
            this.tbMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbMessage.Location = new System.Drawing.Point(12, 406);
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.Size = new System.Drawing.Size(337, 28);
            this.tbMessage.TabIndex = 0;
            // 
            // bSend
            // 
            this.bSend.Enabled = false;
            this.bSend.Location = new System.Drawing.Point(355, 405);
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
            this.bConnect.Location = new System.Drawing.Point(484, 86);
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
            this.bDisconnect.Location = new System.Drawing.Point(639, 86);
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
            this.lbChat.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lbChat.Size = new System.Drawing.Size(466, 384);
            this.lbChat.TabIndex = 4;
            // 
            // tbAdress
            // 
            this.tbAdress.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbAdress.Location = new System.Drawing.Point(484, 52);
            this.tbAdress.Name = "tbAdress";
            this.tbAdress.ReadOnly = true;
            this.tbAdress.Size = new System.Drawing.Size(194, 28);
            this.tbAdress.TabIndex = 5;
            // 
            // tbPort
            // 
            this.tbPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbPort.Location = new System.Drawing.Point(684, 52);
            this.tbPort.Name = "tbPort";
            this.tbPort.ReadOnly = true;
            this.tbPort.Size = new System.Drawing.Size(104, 28);
            this.tbPort.TabIndex = 6;
            // 
            // bFindServer
            // 
            this.bFindServer.Location = new System.Drawing.Point(484, 12);
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
            this.tbUserName.Location = new System.Drawing.Point(484, 177);
            this.tbUserName.Name = "tbUserName";
            this.tbUserName.Size = new System.Drawing.Size(194, 28);
            this.tbUserName.TabIndex = 8;
            // 
            // laUserName
            // 
            this.laUserName.AutoSize = true;
            this.laUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laUserName.Location = new System.Drawing.Point(484, 154);
            this.laUserName.Name = "laUserName";
            this.laUserName.Size = new System.Drawing.Size(94, 20);
            this.laUserName.TabIndex = 9;
            this.laUserName.Text = "User Name";
            // 
            // bAcceptName
            // 
            this.bAcceptName.Enabled = false;
            this.bAcceptName.Location = new System.Drawing.Point(484, 211);
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
            this.cbUsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbUsers.FormattingEnabled = true;
            this.cbUsers.Location = new System.Drawing.Point(484, 406);
            this.cbUsers.Name = "cbUsers";
            this.cbUsers.Size = new System.Drawing.Size(304, 28);
            this.cbUsers.TabIndex = 11;
            // 
            // tbID
            // 
            this.tbID.Enabled = false;
            this.tbID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbID.Location = new System.Drawing.Point(684, 177);
            this.tbID.Name = "tbID";
            this.tbID.ReadOnly = true;
            this.tbID.Size = new System.Drawing.Size(104, 28);
            this.tbID.TabIndex = 12;
            // 
            // laUserID
            // 
            this.laUserID.AutoSize = true;
            this.laUserID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laUserID.Location = new System.Drawing.Point(680, 154);
            this.laUserID.Name = "laUserID";
            this.laUserID.Size = new System.Drawing.Size(67, 20);
            this.laUserID.TabIndex = 13;
            this.laUserID.Text = "User ID";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
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
    }
}

