using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Windows;
using System.Windows.Media;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf;
using Telerik.Windows.Documents.Fixed.Model;
using Telerik.Windows.Documents.Fixed.Model.ColorSpaces;
using Telerik.Windows.Documents.Fixed.Model.Editing;
using UploadingToWebApi.Web.Helpers;
using UploadingToWebApi.Web.Models;
using Size = System.Windows.Size;
using Thickness = System.Windows.Thickness;

namespace UploadingToWebApi.Web.Controllers
{
    public class PdfGeneratorController : ApiController
    {
        public static readonly Size PageSize = new Size(Telerik.Windows.Documents.Media.Unit.MmToDip(210), Telerik.Windows.Documents.Media.Unit.MmToDip(297));
        public static readonly Thickness Margins = new Thickness(Telerik.Windows.Documents.Media.Unit.MmToDip(10));
        public static readonly Size RemainingPageSize = new Size(PageSize.Width - Margins.Left - Margins.Right, PageSize.Height - Margins.Top - Margins.Bottom);
        
        public IEnumerable<string> Get()
        {
            return new[] { "This controller... ", "is up and running" };
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] MyPdfContent value)
        {
            RadFixedDocument document;

            // determine if we're using images or not and generate the document accordingly
            if (string.IsNullOrEmpty(value.ImageBase64))
            {
                // if there's no image, generate a sample with graphics
                document = GenerateSampleDocument(value);
            }
            else
            {
                // if there is image data, insert the image
                document = GenerateImageDocument(value);
            }

            // Now we export the RadDocument as a PDF file and save it to the server
            var provider = new PdfFormatProvider();
            provider.Export(document);
            byte[] bytes = provider.Export(document);

            var response = new HttpResponseMessage();
            response.Content = new ByteArrayContent(bytes);
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = value.RequestedFileName;
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentLength = bytes.Length;

            return response;
        }

        /// <summary>
        /// Creates a same RadFixedDocument using details text from uploaded content
        /// </summary>
        /// <param name="value">the content to use</param>
        /// <returns>FixedDocument that can be exported asa PDF file</returns>
        private static RadFixedDocument GenerateSampleDocument(MyPdfContent value)
        {
            double defaultLeftIndent = 50;
            double defaultLineHeight = 16;

            var document = new RadFixedDocument();
            var page = document.Pages.AddPage();
            page.Size = PageSize;
            
            var editor = new FixedContentEditor(page);
            editor.Position.Translate(defaultLeftIndent, 50);

            double currentTopOffset = 110;
            editor.Position.Translate(defaultLeftIndent, currentTopOffset);
            double maxWidth = page.Size.Width - defaultLeftIndent * 2;

            DocumentHelpers.DrawDescription(editor, maxWidth);

            currentTopOffset += defaultLineHeight * 4;
            editor.Position.Translate(defaultLeftIndent, currentTopOffset);

            using (editor.SaveProperties())
            {
                DocumentHelpers.DrawFunnelFigure(editor);
            }

            // use the uploaded text
            DocumentHelpers.DrawText(editor, maxWidth, value);

            return document;
        }

        /// <summary>
        /// Creates a RadFixedDocument using an image from uploaded content
        /// </summary>
        /// <param name="value">the content to use</param>
        /// <returns>FixedDocument that can be exported asa PDF file</returns>
        private static RadFixedDocument GenerateImageDocument(MyPdfContent value)
        {
            // Load the image data into an ImageSource object
            Telerik.Windows.Documents.Fixed.Model.Resources.ImageSource imageSource;

            using (var imgStream = new MemoryStream(Convert.FromBase64String(value.ImageBase64)))
            {
                imageSource = new Telerik.Windows.Documents.Fixed.Model.Resources.ImageSource(imgStream);
            }

            // instantiate the document and add a page
            var document = new RadFixedDocument();
            var page = document.Pages.AddPage();
            page.Size = PageSize;
            
            // instantiate an editor, this is what writes all the content to the page
            var editor = new FixedContentEditor(page);
            editor.GraphicProperties.StrokeThickness = 0;
            editor.GraphicProperties.IsStroked = false;
            
            try
            {
                // use the uploaded value for background color
                var bgColor = (Color)ColorConverter.ConvertFromString(value.BackgroundColor);
                editor.GraphicProperties.FillColor = new RgbColor(bgColor.R, bgColor.G, bgColor.B);
            }
            catch
            {
                editor.GraphicProperties.FillColor = new RgbColor(255, 255, 255);
            }
            
            editor.DrawRectangle(new Rect(0, 0, PageSize.Width, PageSize.Height));
            editor.Position.Translate(Margins.Left, Margins.Top);

            // Description text
            var block = new Block();
            block.HorizontalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.HorizontalAlignment.Center;
            block.TextProperties.FontSize = 22;

            // use the uploaded content for the title
            block.InsertText(value.Title);

            var blockSize = block.Measure(RemainingPageSize);
            editor.DrawBlock(block, RemainingPageSize);

            editor.Position.Translate(Margins.Left, blockSize.Height + Margins.Top + 20);

            // NOTE - This is where the ImageSource is used and drawn onto the document
            var imageBlock = new Block();
            imageBlock.HorizontalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.HorizontalAlignment.Center;
            imageBlock.InsertImage(imageSource);
            editor.DrawBlock(imageBlock, RemainingPageSize);

            return document;
        }
    }
}