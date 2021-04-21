# TseExamples
The core repository for TSE example apps across all DevCraft products.

## 1. Reporting REST API on Linux

This project shows that you can build an ASP.NET application  with .NET 5 and deploy to Linux. This works on any target that .NET Core/.NET 5 has a runtime for (e.g. from Ubuntu to Raspberry Pi OS).

Go to https://docs.microsoft.com/en-us/dotnet/core/install/linux to find runtime installation instructions for your version of Linux.

## 2 UploadingToWebApi

This demo contains three projects. An ASP.NET application with Web APIs and MVC views, and WPF & UWP client applications to interact with the APIs.

### Telerik Reporting REST Service

This is a straightforward demo that hosts a Telerik Reporting REST Service (ReportsController.cs) so that you can see the HTML5 ReportViewer in an MVC View. You can see this demo now at the following link  <a href="http://webapifortelerikdemos.azurewebsites.net/Home/ReportViewerView1" target="_blank">HTML5 ReportViewer in MVC View</a>

### PDF Generator Web API

The PDF Generator WebAPI is an ApiController that will return a PDF file from custom uploaded content. The working being done on the server means you can use any client (UWP, Xamarin.Forms, WPF, etc).

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


#### Example

The Visual Studio solution contains two client demo apps, WPF one and UWP. Here's the UWP client example app rendering the runtime XAML in a PDF document.


![image](https://user-images.githubusercontent.com/3520532/47941263-d3e83680-dec3-11e8-8020-148c385cb11e.png)