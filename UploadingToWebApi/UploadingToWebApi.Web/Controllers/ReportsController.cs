using System.Web.Http.Cors;
using System.IO;
using System.Web;
using Telerik.Reporting.Cache.File;
using Telerik.Reporting.Services;
using Telerik.Reporting.Services.WebApi;

namespace UploadingToWebApi.Web.Controllers
{
    //The class name determines the service URL. 
    //ReportsController class name defines /api/report/ service URL.
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ReportsController : ReportsControllerBase
    {
        static readonly ReportServiceConfiguration configurationInstance;

        static ReportsController()
        {
            //Setup the ReportServiceConfiguration
            configurationInstance = new ReportServiceConfiguration
            {
                HostAppId = "Html5App",
                Storage = new FileStorage(),
                ReportSourceResolver = new UriReportSourceResolver(Path.Combine(HttpContext.Current.Server.MapPath("~/"), "Reports"))
            };
        }

        public ReportsController()
        {
            //Initialize the service configuration
            this.ReportServiceConfiguration = configurationInstance;
        }
    }
}