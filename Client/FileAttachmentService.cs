using Client.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public class FileAttachmentService
    {
        private const long MAX_FILE_SIZE = 20000000;
        private const long MAX_TOTAL_FILE_SIZE = 50000000;
        private readonly List<string> UNACCEPTABLE_EXTENSIONS;

        public Dictionary<int, string> Files { get; private set; }

        private HttpClientService httpService;
        private int userID;
        private long totalFilesSize;

        private event Action UpdateFilesList;

        public FileAttachmentService(int id)
        {
            httpService = new HttpClientService();

            UNACCEPTABLE_EXTENSIONS.Add(".exe");
            UNACCEPTABLE_EXTENSIONS.Add(".png");
            UNACCEPTABLE_EXTENSIONS.Add(".bin");

            userID = id;
            totalFilesSize = 0;
            Files = new Dictionary<int, string>();
        }

        public void SubscribeHandler(Action handler)
        {
            UpdateFilesList += handler;
        }

        public void UploadFile(string path)
        {
            var fileInfo = new FileInfo(path);

            try
            {
                if (fileInfo.Length <= MAX_FILE_SIZE)
                {
                    if (fileInfo.Length + totalFilesSize <= MAX_TOTAL_FILE_SIZE)
                    {
                        if (!UNACCEPTABLE_EXTENSIONS.Contains(fileInfo.Extension))
                        {
                            int fileID = httpService.AddFileAsync(path, userID).Result;
                            Files.Add(fileID, fileInfo.Name);
                            totalFilesSize += fileInfo.Length;

                            UpdateFilesList?.Invoke();

                            MessageBox.Show("File " + fileInfo.Name + " was uploaded with id = " + fileID.ToString());
                        }
                        else
                        {
                            MessageBox.Show("Chosen file has unacceptable extension");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Total files size is over maximum limit");
                    }
                }
                else
                {
                    MessageBox.Show("File's size is over maximum limit");
                }
            }
            catch
            {
                MessageBox.Show("Something unexpected happened");
            }
        }

        public void DeleteFile(int fileID)
        {
            try
            {
                if (httpService.DeleteFileAsync(fileID).Result)
                {
                    MessageBox.Show("File was deleted successfully");
                }
                else
                {
                    MessageBox.Show("File with such id has been already deleted");
                }
            }
            catch
            {
                MessageBox.Show("Something unexpected happened");
            }
        }

        public void GetFileInfo(int fileID)
        {
            try
            {
                Http.FileAttributes attributes = httpService.GetFileAttributesAsync(fileID).Result;

                if (attributes != null)
                {
                    MessageBox.Show("File name: " + attributes.Name + "\nFile size: " + attributes.Size.ToString() + " bytes");
                }
                else
                {
                    MessageBox.Show("There is no file with such id");
                }
            }
            catch
            {
                MessageBox.Show("Something unexpected happened");
            }
        }

        public void DownloadFile(string path, int fileID)
        {
            try
            {
                using (var fs = new FileStream(path, FileMode.Create))
                {
                    Stream fileStream = httpService.GetFileAsync(fileID).Result;

                    if (fileStream != null)
                    {
                        fileStream.CopyTo(fs);
                    }
                    else
                    {
                        MessageBox.Show("There is no file with such id, maybe it has been removed");
                    }
                }
            }
            catch
            {
                MessageBox.Show("Something unexpected happened");
            }
        }
    }
}
