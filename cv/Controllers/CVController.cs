using System;
using System.Configuration;
using System.Web.Hosting;
using System.Web.Mvc;
using cv.db;
using SelectPdf;

namespace cv.Controllers
{
    public class CvController : Controller
    {
        private readonly string _cv = System.IO.File.ReadAllText(HostingEnvironment.MapPath(FilePath.CV));
        private readonly ILogRepository _logRepo = new LogRepository();

        public void GetPdf()
        {
            _logRepo.Add(new LogItem(HttpContext.Request, "get Pdf"));
            var response = System.Web.HttpContext.Current.Response;

            var converter = new HtmlToPdf();
            converter.Options.MarginTop = 50;
            converter.Options.MarginLeft = 50;
            converter.Options.MarginRight = 50;
            converter.Options.MarginBottom = 50;

            var doc = converter.ConvertHtmlString(_cv);
            doc.Save(response, false, string.Format(ConfigurationManager.AppSettings["FileName"], DateTime.Now.ToString(ConfigurationManager.AppSettings["DateFormat"])));
            doc.Close();
        }

        public ActionResult Index()
        {
            _logRepo.Add(new LogItem(HttpContext.Request, "check CV"));
            ViewBag.cv = _cv;
            ViewBag.Title = ConfigurationManager.AppSettings["Title"];
            return View();
        }
    }
}