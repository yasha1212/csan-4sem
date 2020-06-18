using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage
{
    public class StorageServer
    {
        private const string SERVER_URI = "http://localhost:8080/";
        private const string STORAGE_PATH = @"D:\University\GitRepos\csan-4sem-1\RealStorage\";

        private int fileID;
        private Dictionary<int, FileAttributes> files;

        public StorageServer()
        {
            fileID = 0;
            files = new Dictionary<int, FileAttributes>();
        }

        public void Start()
        {
            HttpListener listener = new HttpListener();

            listener.Prefixes.Add(SERVER_URI);
            listener.Start();

            while(true)
            {
                HandleRequestAsync(listener.GetContext());
            }
        }

        private async void HandleRequestAsync(HttpListenerContext context)
        {
            await Task.Run(() => HandleRequest(context));
        }

        private void HandleRequest(HttpListenerContext context)
        {
            switch (context.Request.HttpMethod)
            {
                case "GET":
                    GetFile(context);
                    break;
                case "POST":
                    AddFile(context);
                    break;
                case "DELETE":
                    DeleteFile(context);
                    break;
                case "HEAD":
                    GetFileAttrubutes(context);
                    break;
            }
        }

        private void DeleteFile(HttpListenerContext context)
        {

        }

        private void AddFile(HttpListenerContext context)
        {
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            string fileName = request.QueryString.Get("filename");
            string userID = request.QueryString.Get("userid");
            string filePath = STORAGE_PATH + userID + @"\" + fileName;

            if (Directory.Exists(STORAGE_PATH + userID + @"\"))
            {
                if (!File.Exists(filePath))
                {
                    using (var fs = new FileStream(filePath, FileMode.CreateNew))
                    {
                        request.InputStream.CopyTo(fs);

                        var fileInfo = new FileInfo(filePath);
                        files.Add(fileID++, new FileAttributes(fileInfo.Length, fileInfo.Name, fileInfo.FullName));

                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.Close();
                    }
                }
                else
                {
                    foreach (var id in files.Keys)
                    {
                        if (files[id].Name == fileName)
                        {
                            response.StatusCode = (int)HttpStatusCode.InternalServerError;

                            byte[] data = Encoding.Unicode.GetBytes(id.ToString());

                            response.OutputStream.Write(data, 0, data.Length);
                            response.Close();

                            break;
                        }
                    }
                }
            }
            else
            {
                Directory.CreateDirectory(STORAGE_PATH + userID + @"\");

                using (var fs = new FileStream(filePath, FileMode.CreateNew))
                {
                    request.InputStream.CopyTo(fs);

                    var fileInfo = new FileInfo(filePath);
                    files.Add(fileID++, new FileAttributes(fileInfo.Length, fileInfo.Name, fileInfo.FullName));

                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Close();
                }
            }
        }

        private void GetFile(HttpListenerContext context)
        {

        }

        private void GetFileAttrubutes(HttpListenerContext context)
        {

        }
    }
}
