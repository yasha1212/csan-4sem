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
        private const string SERVER_URI = "http://localhost:3000/";
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
                    var resourceType = context.Request.QueryString.Get("resourcetype");
                    
                    if (resourceType == "file")
                    {
                        GetFile(context);
                    }
                    else
                    {
                        GetFileAttributes(context);
                    }

                    break;
                case "POST":
                    AddFile(context);
                    break;
                case "DELETE":
                    DeleteFile(context);
                    break;
            }
        }

        private void DeleteFile(HttpListenerContext context)
        {
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            int fileID = int.Parse(request.QueryString.Get("fileid"));

            if (files.ContainsKey(fileID))
            {
                string filePath = files[fileID].Path;

                File.Delete(filePath);
                files.Remove(fileID);
                response.StatusCode = (int)HttpStatusCode.OK;

                Console.WriteLine("File " + files[fileID].Name + " id: " + fileID.ToString() + " has been removed");
            }
            else
            {
                Console.WriteLine("File with id " + fileID.ToString() + " does not exist");
                response.StatusCode = (int)HttpStatusCode.NotFound;
            }

            response.Close();
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
                        files.Add(++fileID, new FileAttributes(fileInfo.Length, fileInfo.Name, fileInfo.FullName));

                        Console.WriteLine("File " + fileName + " id: " + fileID + " has been added");

                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.Close();
                    }
                }
                else
                {
                    Console.WriteLine("File " + fileName + "is already exists");

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

                    Console.WriteLine("File " + fileName + " id: " + fileID + " has been added");

                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Close();
                }
            }
        }

        private void GetFile(HttpListenerContext context)
        {
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            int fileID = int.Parse(request.QueryString.Get("fileid"));

            if (files.ContainsKey(fileID))
            {
                using (var fs = new FileStream(files[fileID].Path, FileMode.Open))
                {
                    fs.CopyTo(response.OutputStream);
                    response.StatusCode = (int)HttpStatusCode.OK;

                    Console.WriteLine("File " + files[fileID].Name + " is downloaded");
                }
            }
            else
            {
                Console.WriteLine("File with id " + fileID.ToString() + " does not exist");

                response.StatusCode = (int)HttpStatusCode.NotFound;
            }

            response.Close();
        }

        private void GetFileAttributes(HttpListenerContext context)
        {
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            int fileID = int.Parse(request.QueryString.Get("fileid"));

            if (files.ContainsKey(fileID))
            {
                FileAttributes attributes = files[fileID];

                response.Headers.Set("name", attributes.Name);
                response.Headers.Set("size", attributes.Size.ToString());
                response.StatusCode = (int)HttpStatusCode.OK;
            }
            else
            {
                Console.WriteLine("File with id " + fileID.ToString() + " does not exist");

                response.StatusCode = (int)HttpStatusCode.NotFound;
            }

            response.Close();
        }
    }
}
