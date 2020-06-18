using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Forms
{
    public partial class MessageFilesForm : Form
    {
        private List<int> files;
        private FileAttachmentService fileService;

        public MessageFilesForm(List<int> files)
        {
            this.files = files;
            fileService = new FileAttachmentService();
            InitializeComponent();
            InitializeListBox();
        }

        private async void InitializeListBox()
        {
            lbFiles.Items.Clear();

            foreach (int fileID in files)
            {
                var attributes = await fileService.GetFileInfo(fileID);
                lbFiles.Items.Add(attributes.Name + " (" + fileID.ToString() + ")");
            }
        }

        private void lbFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbFiles.SelectedIndex != -1)
            {
                bDownloadFile.Enabled = true;
                bGetInfo.Enabled = true;
            }
            else
            {
                bDownloadFile.Enabled = false;
                bGetInfo.Enabled = false;
            }
        }

        private async void bGetInfo_Click(object sender, EventArgs e)
        {
            if (lbFiles.SelectedIndex != -1)
            {
                int fileID = IDExtractor.GetIDFromString(lbFiles.SelectedItem.ToString());

                var attributes = await fileService.GetFileInfo(fileID);

                MessageBox.Show("File name: " + attributes.Name + "\nFile size: " + attributes.Size.ToString() + " bytes");
            }
        }

        private void bDownloadFile_Click(object sender, EventArgs e)
        {
            if (lbFiles.SelectedIndex != -1)
            {
                int fileID = IDExtractor.GetIDFromString(lbFiles.SelectedItem.ToString());

                var saveFileDialog = new SaveFileDialog();
                saveFileDialog.ShowDialog();
                string path = saveFileDialog.FileName;

                fileService.DownloadFile(path, fileID);
            }
        }
    }
}
