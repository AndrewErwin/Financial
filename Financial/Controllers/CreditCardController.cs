using System;
using System.Web.Mvc;
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
        [Route("Details/CreditCard/{id}/invoice/{month}/{year}")]
        [Route("Details/CreditCard/{id}/invoice/{month}")]
        [Route("Details/CreditCard/{id}")]
        [Route("CreditCard/Details/{id}/invoice/{month}/{year}", Name = "DetailsInvoiceCreditCard")]
        [Route("CreditCard/Details/{id}/invoice/{month}")]
        [Route("CreditCard/Details/{id}", Name = "DetailsCreditCard")]
        public ActionResult Details(String id, int? month, int? year)
        {

            CreditCard creditCard = creditCardDAO.GetById(creditCardDAO.TryParseToEntityId(id), c => c.Network);
            if (creditCard != null)
            {
                ViewData.Add("Invoice", creditCardDAO.GetInvoice(creditCard.Id, month, year));
                return View(creditCard);
            }

            TempData["card_not_found"] = id;
            return RedirectToRoute("ListCreditCards");
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

        [HttpGet]
        [Route("Edit/CreditCard/{id}")]
        [Route("CreditCard/Edit/{id}", Name = "EditCreditCard")]
        public ActionResult Edit(String id)
        {
            CreditCard creditCard = creditCardDAO.GetById(creditCardDAO.TryParseToEntityId(id));
            if (creditCard != null)
            {
                ViewBag.NetworkList = ccnetworkDAO.List();
                return View(Mapper.Map<CreditCardFormModel>(creditCard));
            }

            TempData["card_not_found"] = id;
            return RedirectToRoute("ListCreditCards");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/CreditCard")]
        [Route("CreditCard/Edit", Name = "EditCreditCardPost")]
        public ActionResult Edit(CreditCardFormModel modifiedCard)
        {
            if (ModelState.IsValid)
            {
                CreditCard creditCard = Mapper.Map<CreditCard>(modifiedCard);
                creditCardDAO.Update(creditCard);
                TempData["modifiedCard"] = creditCard;
                return RedirectToRoute("ListCreditCards");
            }
            ViewBag.NetworkList = ccnetworkDAO.List();
            return View(modifiedCard);
        }

        [HttpGet]
        [Route("Delete/CreditCard/{id}")]
        [Route("CreditCard/Delete/{id}", Name = "DeleteCreditCard")]
        public ActionResult Delete(String id)
        {
            CreditCard creditCard = creditCardDAO.GetById(creditCardDAO.TryParseToEntityId(id));
            if (creditCard != null)
            {
                return View(Mapper.Map<AnyGuidConfirmDelete>(creditCard));
            }

            TempData["card_not_found"] = id;
            return RedirectToRoute("ListCreditCards");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Delete/CreditCard")]
        [Route("CreditCard/Delete", Name = "DeleteCreditCardPost")]
        public ActionResult Delete(AnyGuidConfirmDelete model)
        {
            CreditCard card = Mapper.Map<CreditCard>(model);
            creditCardDAO.Delete(card);
            TempData["card_deleted"] = model.Name;
            return RedirectToRoute("ListCreditCards");
        }
    }
}