# TSE Examples

A set of demo projects for special edge cases presented to Progress DevTools Support Engineers.

| Workflow      | Build Status |
|---------------|--------------|
| `main`         | [![Build Main](https://github.com/LanceMcCarthy/TseExamples/actions/workflows/main.yml/badge.svg)](https://github.com/LanceMcCarthy/TseExamples/actions/workflows/main.yml)                             |
| `release-linux` | [![Publish Linux Server](https://github.com/LanceMcCarthy/TseExamples/actions/workflows/release-linux.yml/badge.svg)](https://github.com/LanceMcCarthy/TseExamples/actions/workflows/release-linux.yml) |

## Demos

### 1. Reporting REST API on Linux using .NET 5.0

This project shows that you can build an ASP.NET application  with .NET 5 and deploy to Linux. This works on any target that .NET Core/.NET 5 has a runtime for (e.g. from Ubuntu to Raspberry Pi OS).

Go to https://docs.microsoft.com/en-us/dotnet/core/install/linux to find runtime installation instructions for your version of Linux.

### 2 Uploading To WebApi

This demo contains three projects:

- Server Application with Web and REST APIs
  - ASP.NET MVC5
- Client Applications to consume Web APIs
  - WPF
  - UWP

#### Server

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

#### Client

Here's the UWP client example app rendering runtime UI (XAML) as base64 image data, which gets uploaded to the server and inserted in a dynamically created PDF document.


![image](https://user-images.githubusercontent.com/3520532/47941263-d3e83680-dec3-11e8-8020-148c385cb11e.png)
