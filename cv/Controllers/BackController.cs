using System.Configuration;
using System.Linq;
using System.Web.Hosting;
using System.Web.Mvc;
using cv.db;

namespace cv.Controllers
{
    [PrivacyShield]
    public class BackController : Controller
    {
        private static readonly string CvPath = HostingEnvironment.MapPath(FilePath.CV);
        private readonly ILogRepository _logRepo = new LogRepository ();

        public ActionResult Edit()
        {
            return View("Edit", "_LayoutAngular");
        }

        public ActionResult List()
        {
            ViewBag.List = _logRepo.GetAll();
            return View();
        }

        public JsonResult Get()
        {
            return Json(System.IO.File.ReadAllText(CvPath), JsonRequestBehavior.AllowGet);
        }

        public void Save(string cv)
        {
            System.IO.File.WriteAllText(CvPath, cv);
        }
    }

    public class PrivacyShieldAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var allowed = ConfigurationManager.AppSettings["OwnerIps"].Replace(" ","").Split(',');
            if (allowed.Contains(filterContext.HttpContext.Request.UserHostAddress)) return;
            filterContext.Result = new RedirectResult("/");
        }
    }
}