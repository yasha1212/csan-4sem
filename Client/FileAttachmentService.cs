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

        public FileAttachmentService()
        {
            httpService = new HttpClientService();

            UNACCEPTABLE_EXTENSIONS = new List<string>();
            UNACCEPTABLE_EXTENSIONS.Add(".exe");
            UNACCEPTABLE_EXTENSIONS.Add(".png");
            UNACCEPTABLE_EXTENSIONS.Add(".bin");

            userID = 0;
            totalFilesSize = 0;
            Files = new Dictionary<int, string>();
        }

        public void Initialize()
        {
            Files.Clear();
            totalFilesSize = 0;
            userID = 0;
        }

        public void SetID(int userID)
        {
            this.userID = userID;
        }

        public void SubscribeHandler(Action handler)
        {
            UpdateFilesList += handler;
        }

        public async void UploadFile(string path)
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
                            int fileID = await httpService.AddFileAsync(path, userID);
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

        public async void DeleteFile(int fileID)
        {
            try
            {
                var fileInfo = await httpService.GetFileAttributesAsync(fileID);

                if (await httpService.DeleteFileAsync(fileID))
                {
                    totalFilesSize -= fileInfo.Size;
                    Files.Remove(fileID);
                    UpdateFilesList?.Invoke();

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

        public async Task<Http.FileAttributes> GetFileInfo(int fileID)
        {
            try
            {
                Http.FileAttributes attributes = await httpService.GetFileAttributesAsync(fileID);

                if (attributes != null)
                {
                    return attributes;
                }
                else
                {
                    MessageBox.Show("There is no file with such id");
                    return null;
                }
            }
            catch
            {
                MessageBox.Show("Something unexpected happened");
                return null;
            }
        }

        public async void DownloadFile(string path, int fileID)
        {
            try
            {
                using (var fs = new FileStream(path, FileMode.Create))
                {
                    Stream fileStream = await httpService.GetFileAsync(fileID);

                    if (fileStream != null)
                    {
                        fileStream.CopyTo(fs);

                        MessageBox.Show("File has been successfully downloaded");
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
