using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        [Route("Inicio", Name = "Start")]
        public ActionResult Start()
        {
            return View();
        }
    }
}