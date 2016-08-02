﻿using System.Web.Mvc;
using AutoMapper;
using Financial.DAO;
using Financial.Models;
using Financial.Models.Entities;

namespace Financial.Controllers
{
    [Authorize]
    public class CreditCardController : Controller
    {
        private CreditCardDAO creditCardDAO;
        private CreditCardNetworkDAO ccnetworkDAO;

        public CreditCardController(CreditCardDAO creditCardDao, CreditCardNetworkDAO ccnetworkDao)
        {
            this.creditCardDAO = creditCardDao;
            this.ccnetworkDAO = ccnetworkDao;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToRoute("ListCreditCards");
        }

        [HttpGet]
        [Route("CreditCards")]
        [Route("List/CreditCard")]
        [Route("CreditCard/List", Name = "ListCreditCards")]
        public ActionResult List()
        {
            return View(creditCardDAO.List(c => c.Network));
        }

        [HttpGet]
        [Route("New/CreditCard")]
        [Route("CreditCard/New", Name = "NewCreditCard")]
        public ActionResult New()
        {
            ViewBag.NetworkList = ccnetworkDAO.List();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("CreditCard/New", Name = "NewCreditCardPost")]
        public ActionResult New(CreditCardFormModel newCreditCard)
        {
            if (ModelState.IsValid)
            {
                CreditCard creditCard = Mapper.Map<CreditCard>(newCreditCard);
                creditCardDAO.Add(creditCard);
                TempData["newCreditCard"] = creditCard;
                return RedirectToRoute("ListCreditCards");
            }
            ViewBag.NetworkList = ccnetworkDAO.List();
            return View(newCreditCard);
        }

    }
}