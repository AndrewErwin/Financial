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

        [HttpGet]
        [Route("New/Subscription")]
        [Route("Subscription/New", Name = "NewSubscription")]
        public ActionResult New()
        {
            ViewBag.CreditCardList = creditCardDAO.List();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("New/Subscription")]
        [Route("Subscription/New", Name = "NewSubscriptionPost")]
        public ActionResult New(SubscriptionFormModel newSubscription)
        {
            if (ModelState.IsValid)
            {
                Subscription subscription = Mapper.Map<Subscription>(newSubscription);
                subscriptionDAO.Add(subscription);
                TempData["newSubscription"] = subscription;
                return RedirectToRoute("ListSubscriptions");
            }
            ViewBag.CreditCardList = creditCardDAO.List();
            return View(newSubscription);
        }

        [HttpGet]
        [Route("Edit/Subscription/{id}")]
        [Route("Subscription/Edit/{id}", Name = "EditSubscription")]
        public ActionResult Edit(String id)
        {
            Subscription subscription = subscriptionDAO.GetById(subscriptionDAO.TryParseToEntityId(id));
            if (subscription != null)
            {
                ViewBag.CreditCardList = creditCardDAO.List();
                return View(Mapper.Map<SubscriptionFormModel>(subscription));
            }
            TempData["subscription_not_found"] = id;
            return RedirectToRoute("ListSubscriptions");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/Subscription")]
        [Route("Subscription/Edit", Name = "EditSubscriptionPost")]
        public ActionResult Edit(SubscriptionFormModel modifiedSubscription)
        {
            if (ModelState.IsValid)
            {
                Subscription subscription = Mapper.Map<Subscription>(modifiedSubscription);
                subscriptionDAO.Update(subscription);
                TempData["modifiedSubscription"] = subscription;
                return RedirectToRoute("ListSubscriptions");
            }
            ViewBag.CreditCardList = creditCardDAO.List();
            return View(modifiedSubscription);
        }

        [HttpGet]
        [Route("Delete/Subscription/{id}")]
        [Route("Subscription/Delete/{id}", Name = "DeleteSubscription")]
        public ActionResult Delete(String id)
        {
            Subscription subscription = subscriptionDAO.GetById(subscriptionDAO.TryParseToEntityId(id));
            if (subscription != null)
            {
                return View(Mapper.Map<AnyGuidConfirmDelete>(subscription));
            }

            TempData["subscription_not_found"] = id;
            return RedirectToRoute("ListSubscriptions");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Delete/Subscription")]
        [Route("Subscription/Delete", Name = "DeleteSubscriptionPost")]
        public ActionResult Delete(AnyGuidConfirmDelete model)
        {
            Subscription subscription = Mapper.Map<Subscription>(model);
            subscriptionDAO.Delete(subscription);
            TempData["subscription_deleted"] = model.Name;
            return RedirectToRoute("ListSubscriptions");
        }
    }
}