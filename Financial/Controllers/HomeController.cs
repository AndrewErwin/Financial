using System.Web.Mvc;

namespace Financial.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]        
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("Start", Name = "Start")]
        public ActionResult Start()
        {
            return View();
        }
    }
}