using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Financial.DAO;
using Financial.Models;
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

        [HttpGet]
        [Route("New/Purchase")]
        [Route("Purchase/New", Name = "NewPurchase")]
        public ActionResult New()
        {
            ViewBag.CreditCardList = creditCardDAO.List();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Purchase/New", Name = "NewPurchasePost")]
        public ActionResult New(PurchaseFormModel newPurchase)
        {
            if (ModelState.IsValid)
            {
                Purchase purchase = Mapper.Map<Purchase>(newPurchase);
                purchaseDAO.Add(purchase);
                TempData["newPurchase"] = purchase;
                return RedirectToRoute("ListPurchases");
            }
            ViewBag.CreditCardList = creditCardDAO.List();
            return View(newPurchase);
        }

        [HttpGet]
        [Route("Edit/Purchase/{id}")]
        [Route("Purchase/Edit/{id}", Name = "EditPurchase")]
        public ActionResult Edit(String id)
        {
            Guid purchaseId = Guid.Empty;
            if (Guid.TryParse(id, out purchaseId))
            {
                Purchase purchase = purchaseDAO.GetById(purchaseId);
                if (purchase != null)
                {
                    ViewBag.CreditCardList = creditCardDAO.List();
                    return View(Mapper.Map<PurchaseFormModel>(purchase));
                }
            }

            TempData["purchase_not_found"] = id;
            return RedirectToRoute("ListPurchases");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Purchase/Edit", Name = "EditPurchasePost")]
        public ActionResult Edit(PurchaseFormModel modifiedPurchase)
        {
            if (ModelState.IsValid)
            {
                Purchase purchase = Mapper.Map<Purchase>(modifiedPurchase);
                purchaseDAO.Update(purchase);
                TempData["modifiedPurchase"] = purchase;
                return RedirectToRoute("ListPurchases");
            }
            ViewBag.CreditCardList = creditCardDAO.List();
            return View(modifiedPurchase);
        }

        [HttpGet]
        [Route("Delete/Purchase/{id}")]
        [Route("Purchase/Delete/{id}", Name = "DeletePurchase")]
        public ActionResult Delete(String id)
        {
            Guid purchaseId = Guid.Empty;
            if (Guid.TryParse(id, out purchaseId))
            {
                Purchase purchase = purchaseDAO.GetById(purchaseId);
                if (purchase != null)
                {
                    return View(Mapper.Map<AnyGuidConfirmDelete>(purchase));
                }
            }

            TempData["purchase_not_found"] = id;
            return RedirectToRoute("ListPurchases");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Delete/Purchase")]
        [Route("Purchase/Delete", Name = "DeletePurchasePost")]
        public ActionResult Delete(AnyGuidConfirmDelete model)
        {
            Purchase purchase = Mapper.Map<Purchase>(model);
            purchaseDAO.Delete(purchase);
            TempData["purchase_deleted"] = model.Name;
            return RedirectToRoute("ListPurchases");
        }
    }
}