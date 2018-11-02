using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Provider;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using UploadingToWebApi.Uwp.ApiServices;
using UploadingToWebApi.Uwp.Models;

namespace UploadingToWebApi.Uwp
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void UploadTextButton_OnClicked(object sender, RoutedEventArgs e)
        {
            this.BusyIndicator.IsActive = true;
            this.BusyIndicator.Content = "creating content...";

            var contentForPdf = new MyPdfContent
            {
                Title = "Hello World!",
                Body = "This property can be used to set the string content that would be in the body of the PDF document.",
                BackgroundColor = "#FFFFFF",
                RequestedFileName = "HelloWorld.pdf"
            };
            
            this.BusyIndicator.Content = "uploading content and generating pdf...";

            var pdfBytes =  await WebApiService.Instance.GenerateDocumentAsync(contentForPdf);

            if (pdfBytes != null)
            {
                this.BusyIndicator.Content = "saving file...";

                await this.SaveFileAsync(pdfBytes, contentForPdf.RequestedFileName);
            }
            else
            {
                this.OutputLabel.Foreground = new SolidColorBrush(Colors.OrangeRed);
                this.OutputLabel.Text = "Error Uploading Content. Try again.";
            }

            this.BusyIndicator.Content = "";
            this.BusyIndicator.IsActive = false;
        }

        private async void UploadPictureButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.BusyIndicator.IsActive = true;

            this.BusyIndicator.Content = "generating and encoding bitmap...";
            
            // Render the UI as a png and convert to base64
            var base64String = await RenderGridAsync();

            this.BusyIndicator.Content = "creating content...";

            // NOTE: the png is serialized as a base64 string, you can choose whichever method is best for you
            var contentForPdf = new MyPdfContent
            {
                ImageBase64 = base64String,
                Title = "Example: UWP XAML -> PDF",
                Body = "This document is an example of rendering a XAML UI in a PDF document using Telerik Document Processing Libraries.",
                BackgroundColor = "#FFFFFF",
                RequestedFileName = "XamlContentTest.pdf"
            };
            
            this.BusyIndicator.Content = "uploading content and generating pdf...";

            var pdfBytes = await WebApiService.Instance.GenerateDocumentAsync(contentForPdf);

            if (pdfBytes != null)
            {
                this.BusyIndicator.Content = "saving file...";
                await this.SaveFileAsync(pdfBytes, contentForPdf.RequestedFileName);
            }
            else
            {
                this.OutputLabel.Foreground = new SolidColorBrush(Colors.OrangeRed);
                this.OutputLabel.Text = "Error Uploading Content. Try again.";
            }

            this.BusyIndicator.Content = "";
            this.BusyIndicator.IsActive = false;
        }


        private async Task<string> RenderGridAsync()
        {
            // renders UIElement into a bitmap
            var rtb = new RenderTargetBitmap();
            await rtb.RenderAsync(this.ChartGrid);

            // get the pixels from the rendered UI elements
            var pixelBuffer = await rtb.GetPixelsAsync();
            var pixels = pixelBuffer.ToArray();
            
            // Encode the pixels into a png
            var randomAccessStream = new InMemoryRandomAccessStream();
            var bitmapEncoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, randomAccessStream);
            bitmapEncoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Straight, (uint)rtb.PixelHeight, (uint)rtb.PixelWidth, 96, 96, pixels);
            await bitmapEncoder.FlushAsync();
            randomAccessStream.Seek(0);
            var encodedImageBytes = new byte[randomAccessStream.Size];
            await randomAccessStream.AsStream().ReadAsync(encodedImageBytes, 0, encodedImageBytes.Length);

            // convert the png byte[] into base64
            var base64String = Convert.ToBase64String(encodedImageBytes);
            return base64String;
        }
        
        private async Task SaveFileAsync(byte[] fileContents, string fileName)
        {
            var savePicker = new FileSavePicker();
            savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            savePicker.FileTypeChoices.Add("PDF", new List<string> { ".pdf" });
            savePicker.SuggestedFileName = fileName;

            var file = await savePicker.PickSaveFileAsync();

            if (file != null)
            {
                CachedFileManager.DeferUpdates(file);

                await FileIO.WriteBytesAsync(file, fileContents);

                var status = await CachedFileManager.CompleteUpdatesAsync(file);

                if (status == FileUpdateStatus.Complete)
                {
                    var kb = fileContents.Length / 1024;

                    this.OutputLabel.Foreground = new SolidColorBrush(Colors.LimeGreen);
                    this.OutputLabel.Text = $"{file.Name} was saved ({kb:N0} KB).";
                }
                else
                {
                    this.OutputLabel.Foreground = new SolidColorBrush(Colors.OrangeRed);
                    this.OutputLabel.Text = "{file.Name} couldn't be saved.";
                }
            }
            else
            {
                this.OutputLabel.Foreground = new SolidColorBrush(Colors.Black);
                this.OutputLabel.Text = "Operation cancelled.";
            }
        }
    }
}
