using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Telerik.Windows.Cloud;

namespace UploadingToWebApi.Wpf.Providers
{
    public class MyWebApiProvider : ICloudUploadProvider
    {
        public async Task<object> UploadFileAsync(string fileName, Stream fileStream, CloudUploadFileProgressChanged uploadProgressChanged, CancellationToken cancellationToken)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://webapifortelerikdemos.azurewebsites.net/");

                using (var content = new MultipartFormDataContent())
                using (var fileContent = new StreamContent(fileStream, (int)fileStream.Length))
                {
                    fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = fileName
                    };

                    content.Add(fileContent);

                    using (var message = await client.PostAsync("/api/Values", content, cancellationToken))
                    {
                        var result = await message.Content.ReadAsStringAsync();

                        Debug.WriteLine($"Result - StatusCode: {message.StatusCode}, File Saved To: {result}");

                        // typical output
                        // Result - StatusCode: 201, File Saved To: ["D:\\home\\site\\wwwroot\\Snag1.gif"]

                        return result;
                    }
                }
            }
        }
    }
}
