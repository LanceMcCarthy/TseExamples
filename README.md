# TSE Examples

A set of demo projects for special edge cases presented to Progress DevTools Support Engineers.

> This repo has been phased out, replaced by product specific repos such as https://github.com/LanceMcCarthy/CustomMauiExamples and https://github.com/LanceMcCarthy/CustomXamarinDemos or https://github.com/LanceMcCarthy/DevOpsExamples

### Project: UploadingToWebApi

This demo contains three projects:

- Server Application with Web and REST APIs
  - ASP.NET MVC5
- Client Applications that use the Web APIs
  - WPF
  - UWP

It is available at https://webapifortelerikdemos.azurewebsites.net/

#### Server-side

**Telerik Reporting REST Service**

This is a straightforward demo that hosts a Telerik Reporting REST Service (ReportsController.cs) so that you can see the HTML5 ReportViewer in an MVC View ([visit the live demo](https://webapifortelerikdemos.azurewebsites.net/Home/ReportViewerView1)).

**PDF Generator Web API**

The PDF Generator WebAPI is an ApiController that will return a PDF file from custom uploaded content in the form of JSON data. This means you can use any client application to create the PDF.

```csharp
public class MyPdfContent
{
    public string Title { get; set; }
    public string Body { get; set; }
    public string BackgroundColor { get; set; }
    public string RequestedFileName { get; set; }
    public string ImageBase64 { get; set; }
}
```

#### Client-side

Here's the UWP client example app rendering runtime UI (XAML) as base64 image data, which gets uploaded to the server and inserted in a dynamically created PDF document.

![image](https://user-images.githubusercontent.com/3520532/47941263-d3e83680-dec3-11e8-8020-148c385cb11e.png)
