using System.Web.Mvc;

namespace UploadingToWebApi.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            
            return View();
        }

        public ActionResult ReportViewerView1()
        {
            ViewBag.Title = "HTML5 ReportViewer View";

            return View();
        }
    }
}
