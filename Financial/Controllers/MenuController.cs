using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Financial.Controllers
{
    [ChildActionOnly]
    public class MenuController : Controller
    {
        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult LoginControl()
        {
            return PartialView();
        }

        public ActionResult UserControl()
        {
            return PartialView();
        }

        public ActionResult Navigation()
        {
            return PartialView();
        }
    }
}