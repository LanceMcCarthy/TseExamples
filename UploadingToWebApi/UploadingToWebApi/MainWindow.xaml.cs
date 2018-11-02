using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows;
using Newtonsoft.Json;
using Telerik.Windows.Documents.Fixed;
using UploadingToWebApi.Wpf.Models;

namespace UploadingToWebApi.Wpf
{
    public partial class MainWindow : Window
    {
        private readonly HttpClient _client;

        public MainWindow()
        {
            InitializeComponent();

            //CloudUpload.Provider = new MyWebApiProvider();
            //CloudUpload.AutoStart = true;

            _client = new HttpClient {BaseAddress = new Uri(ServiceConstants.ServiceBaseUrl)};
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/pdf"));
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/bson")); // can be used to skip json conversion
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var pdfContent = new MyPdfContent
            {
                Title = "Hello World!",
                Body = "This is the body of the PDF document, and where I'd put some general text",
                BackgroundColor = "#FFFFFF",
                RequestedFileName = "HelloWorld.pdf"
            };
            
            OutputLabel.Text = "Generating...";

            // Serialize the content
            var bodyContent = new StringContent(JsonConvert.SerializeObject(pdfContent), Encoding.UTF8, "application/json");

            // post it to the API method that will generate the PDF
            using (var response = await _client.PostAsync(ServiceConstants.PdfGeneratorApi, bodyContent))
            {
                if (!response.IsSuccessStatusCode)
                {
                    OutputLabel.Text = $"Error: {response.StatusCode}";
                }
                else
                {

                    var bytes = await response.Content.ReadAsByteArrayAsync();
                    
                    using (var pdfStream = new MemoryStream(bytes))
                    {
                        pdfViewer.DocumentSource = new PdfDocumentSource(pdfStream);

                        OutputLabel.Text = $"Complete: Size: {pdfStream.Length}";
                    }
                    
                }
            }
        }
    }
}