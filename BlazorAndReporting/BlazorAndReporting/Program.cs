
using BlazorAndReporting.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Telerik.Reporting.Cache.File;
using Telerik.Reporting.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddTelerikBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

// --- For Telerik Reporting --- //

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.TryAddSingleton<IReportServiceConfiguration>(sp =>
    new ReportServiceConfiguration
    {
        ReportingEngineConfiguration = sp.GetService<IConfiguration>(),
        HostAppId = "ReportingNet6",
        Storage = new FileStorage(),
        ReportSourceResolver = new UriReportSourceResolver(System.IO.Path.Combine(sp.GetService<IWebHostEnvironment>().ContentRootPath, "Reports"))
    });
builder.Services.AddCors(corsOption => corsOption.AddPolicy(
    "ReportingRestPolicy",
    corsBuilder =>
    {
        corsBuilder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    }
));

// ----------------------------- //


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// --- For Telerik Reporting --- //

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.UseCors("ReportingRestPolicy");

// ----------------------------- //

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
