using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Http
{
    public class HttpClientService
    {
        private const string SERVER_URI = "http://localhost:3000/";

        private HttpClient httpClient;

        public HttpClientService()
        {
            httpClient = new HttpClient();
        }

        public async Task<int> AddFileAsync(string path, int userID)
        {
            HttpResponseMessage response;
            int duplicateNumber = 0;

            do
            {
                using (var fs = new FileStream(path, FileMode.Open))
                {
                    var fsContent = new StreamContent(fs);
                    string request;

                    if (duplicateNumber == 0)
                    {
                        string fileName = Path.GetFileName(path);
                        request = SERVER_URI + "?filename=" + fileName + "&userid=" + userID.ToString();
                    }
                    else
                    {
                        string fileName = Path.GetFileNameWithoutExtension(path);
                        string extension = Path.GetExtension(path);

                        request = SERVER_URI + "?filename=" + fileName + "_" + duplicateNumber   + extension + "&userid=" + userID.ToString();
                    }

                    duplicateNumber++;
                    response = await httpClient.PostAsync(request, fsContent);

                    fsContent.Dispose();
                }
            }
            while (response.StatusCode != HttpStatusCode.OK);

            return int.Parse(response.Headers.GetValues("fileid").ToList()[0]);
        }

        public async Task<bool> DeleteFileAsync(int fileID)
        {
            string request = SERVER_URI + "?fileid=" + fileID.ToString();

            var response = await httpClient.DeleteAsync(request);

            return response.IsSuccessStatusCode ? true : false;
        }

        public async Task<Stream> GetFileAsync(int fileID)
        {
            string request = SERVER_URI + "?fileid=" + fileID.ToString() + "&resourcetype=file";

            var response = await httpClient.GetAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStreamAsync();
            }
            else
            {
                return null;
            }
        }

        public async Task<FileAttributes> GetFileAttributesAsync(int fileID)
        {
            string request = SERVER_URI + "?fileid=" + fileID.ToString() + "&resourcetype=attributes";

            var response = await httpClient.GetAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return new FileAttributes(int.Parse(response.Headers.GetValues("size").ToList()[0]), response.Headers.GetValues("name").ToList()[0]);
            }
            else
            {
                return null;
            }
        }
    }
}
