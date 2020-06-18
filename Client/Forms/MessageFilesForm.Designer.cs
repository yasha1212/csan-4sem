namespace Client.Forms
{
    partial class MessageFilesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbFiles = new System.Windows.Forms.ListBox();
            this.bGetInfo = new System.Windows.Forms.Button();
            this.bDownloadFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbFiles
            // 
            this.lbFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbFiles.FormattingEnabled = true;
            this.lbFiles.IntegralHeight = false;
            this.lbFiles.ItemHeight = 25;
            this.lbFiles.Location = new System.Drawing.Point(12, 12);
            this.lbFiles.Name = "lbFiles";
            this.lbFiles.Size = new System.Drawing.Size(304, 398);
            this.lbFiles.TabIndex = 19;
            this.lbFiles.SelectedIndexChanged += new System.EventHandler(this.lbFiles_SelectedIndexChanged);
            // 
            // bGetInfo
            // 
            this.bGetInfo.Enabled = false;
            this.bGetInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bGetInfo.Location = new System.Drawing.Point(322, 12);
            this.bGetInfo.Name = "bGetInfo";
            this.bGetInfo.Size = new System.Drawing.Size(287, 48);
            this.bGetInfo.TabIndex = 23;
            this.bGetInfo.Text = "Get file info";
            this.bGetInfo.UseVisualStyleBackColor = true;
            this.bGetInfo.Click += new System.EventHandler(this.bGetInfo_Click);
            // 
            // bDownloadFile
            // 
            this.bDownloadFile.Enabled = false;
            this.bDownloadFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bDownloadFile.Location = new System.Drawing.Point(322, 66);
            this.bDownloadFile.Name = "bDownloadFile";
            this.bDownloadFile.Size = new System.Drawing.Size(287, 48);
            this.bDownloadFile.TabIndex = 24;
            this.bDownloadFile.Text = "Download file";
            this.bDownloadFile.UseVisualStyleBackColor = true;
            this.bDownloadFile.Click += new System.EventHandler(this.bDownloadFile_Click);
            // 
            // MessageFilesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 422);
            this.Controls.Add(this.bDownloadFile);
            this.Controls.Add(this.bGetInfo);
            this.Controls.Add(this.lbFiles);
            this.Name = "MessageFilesForm";
            this.Text = "MessageFilesForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbFiles;
        private System.Windows.Forms.Button bGetInfo;
        private System.Windows.Forms.Button bDownloadFile;
    }
}