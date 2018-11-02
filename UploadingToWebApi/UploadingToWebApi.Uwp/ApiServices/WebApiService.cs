using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Newtonsoft.Json;
using UploadingToWebApi.Uwp.Models;

namespace UploadingToWebApi.Uwp.ApiServices
{
    public class WebApiService
    {
        #region singleton members

        private static WebApiService _instance;

        public static WebApiService Instance => _instance ?? (_instance = new WebApiService());
        
        #endregion

        private readonly HttpClient _client;

        public WebApiService()
        {
            _client = new HttpClient { BaseAddress = new Uri(ServiceConstants.ServiceBaseUrl) };
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/pdf"));
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/bson")); // can be used to skip json conversion
        }
        
        public async Task<byte[]> GenerateDocumentAsync(MyPdfContent content)
        {
            // serialize into json  todo - use BSON and skip json conversion
            var json = JsonConvert.SerializeObject(content);

            // StringContent for POST 
            var bodyContent = new StringContent(json, Encoding.UTF8, "application/json");

            // POST the StringContent to the controller that generates a PDF and return the file byte[]
            using (var response = await _client.PostAsync(ServiceConstants.PdfGeneratorApi, bodyContent))
            {
                if (response.IsSuccessStatusCode)
                {
                    // Return the byte[] of the PDF file.
                    var bytes = await response.Content.ReadAsByteArrayAsync();
                    return bytes;
                }
                else
                {
                    await new MessageDialog($"Error: {response.StatusCode}", "Upload error").ShowAsync();
                    return null;
                }
            }
        }
    }
}
