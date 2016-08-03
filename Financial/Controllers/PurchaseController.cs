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
    public class PurchaseController : Controller
    {
        private PurchaseDAO purchaseDAO;
        private CreditCardDAO creditCardDAO;

        public PurchaseController(PurchaseDAO purchaseDao, CreditCardDAO creditCardDao)
        {
            this.purchaseDAO = purchaseDao;
            this.creditCardDAO = creditCardDao;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToRoute("ListPurchases");
        }

        [HttpGet]
        [Route("Purchases")]
        [Route("List/Purchase")]
        [Route("Purchase/List", Name = "ListPurchases")]
        public ActionResult List()
        {
            return View(purchaseDAO.List(p => p.CreditCard));
        }
    }
}