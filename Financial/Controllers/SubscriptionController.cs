using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Financial.DAO;
using Financial.Models.Entities;

namespace Financial.Controllers
{
    [Authorize]
    public class SubscriptionController : Controller
    {
        private SubscriptionDAO subscriptionDAO;
        private CreditCardDAO creditCardDAO;

        public SubscriptionController(SubscriptionDAO subscriptionDao, CreditCardDAO creditCardDao)
        {
            this.subscriptionDAO = subscriptionDao;
            this.creditCardDAO = creditCardDao;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToRoute("ListSubscriptions");
        }

        [HttpGet]
        [Route("Subscriptions")]
        [Route("List/Subscription")]
        [Route("Subscription/List", Name = "ListSubscriptions")]
        public ActionResult List()
        {
            return View(subscriptionDAO.List(s => s.CreditCard));
        }
    }
}