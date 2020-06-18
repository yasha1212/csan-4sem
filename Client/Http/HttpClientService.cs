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
        private const string SERVER_URI = "http://localhost:8080/";

        private HttpClient httpClient;

        public HttpClientService()
        {
            httpClient = new HttpClient();
        }

        public async Task<int> AddFile(string path, int userID)
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

                        request = SERVER_URI + "?filename=" + fileName + "_" + duplicateNumber + "." + extension + "&userid=" + userID.ToString();
                    }

                    duplicateNumber++;
                    response = await httpClient.PostAsync(request, fsContent);

                    fsContent.Dispose();
                }
            }
            while (response.StatusCode != HttpStatusCode.OK);

            return -1;
        }
    }
}
