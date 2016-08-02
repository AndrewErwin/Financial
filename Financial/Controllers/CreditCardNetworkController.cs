using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Financial.DAO;
using Financial.Internationalization.Entities.CreditCardNetwork;
using Financial.Models.Entities;

namespace Financial.Controllers
{
    [Authorize]
    public class CreditCardNetworkController : Controller
    {
        private CreditCardNetworkDAO ccnetworkDAO;

        public CreditCardNetworkController(CreditCardNetworkDAO ccnetworkDao)
        {
            this.ccnetworkDAO = ccnetworkDao;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToRoute("ListCreditCardNetworks");
        }

        [HttpGet]
        [Route("CreditCardNetworks")]
        [Route("List/CreditCardNetwork")]
        [Route("CreditCardNetwork/List", Name = "ListCreditCardNetworks")]
        public ActionResult List()
        {
            return View(ccnetworkDAO.List());
        }

        [HttpGet]
        [Route("New/CreditCardNetwork")]
        [Route("CreditCardNetwork/New", Name = "NewCreditCardNetwork")]
        public ActionResult New()
        {
            ViewBag.ImageList = this.GetImageList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("New/CreditCardNetwork")]
        [Route("CreditCardNetwork/New", Name = "NewCreditCardNetworkPost")]
        public ActionResult New(CreditCardNetwork newNetwork)
        {
            if (ModelState.IsValid)
            {
                ccnetworkDAO.Add(newNetwork);
                TempData["newNetwork"] = newNetwork;
                return RedirectToRoute("ListCreditCardNetworks");
            }
            return View(newNetwork);
        }

        [HttpGet]
        [Route("Edit/CreditCardNetwork/{id}")]
        [Route("CreditCardNetwork/Edit/{id}", Name = "EditCreditCardNetwork")]
        public ActionResult Edit(String id)
        {
            Guid networkId = Guid.Empty;
            Guid.TryParse(id, out networkId);
            CreditCardNetwork ccnetwork = ccnetworkDAO.GetById(networkId);
            if (ccnetwork != null)
            {
                ViewBag.ImageList = this.GetImageList();
                return View(ccnetwork);
            }
            else
            {
                TempData["network_not_found"] = id;
                return RedirectToRoute("ListCreditCardNetworks");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/CreditCardNetwork")]
        [Route("CreditCardNetwork/Edit", Name = "EditCreditCardNetworkPost")]
        public ActionResult Edit(CreditCardNetwork modifiedNetwork)
        {
            if (ModelState.IsValid)
            {
                ccnetworkDAO.Update(modifiedNetwork);
                TempData["modifiedNetwork"] = modifiedNetwork;
                return RedirectToRoute("ListCreditCardNetworks");
            }
            ViewBag.ImageList = this.GetImageList();
            return View(modifiedNetwork);
        }

        private List<String> GetImageList()
        {
            List<String> images = new List<string>();
            #region :: get images ::
            DirectoryInfo images_folder = new DirectoryInfo(Server.MapPath("~/Content/images/ccnetworks/"));
            FileInfo[] image_files = images_folder.GetFiles();
            foreach (var image in image_files)
            {
                if (!image.Name.EndsWith(".db"))
                {
                    images.Add(image.Name);
                }
            }
            #endregion
            return images;
        }
    }
}