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
        [Route("List/CreditCardNetwork")]
        [Route("CreditCardNetwork/List")]
        [Route("CreditCardNetworks", Name = "ListCreditCardNetworks")]
        public ActionResult List()
        {
            return View(ccnetworkDAO.List());
        }

        [HttpGet]
        [Route("CreditCardNetwork/New")]
        [Route("New/CreditCardNetwork", Name = "NewCreditCardNetwork")]
        public ActionResult New()
        {
            ViewBag.ImageList = this.GetImageList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("CreditCardNetwork/New")]
        [Route("New/CreditCardNetwork", Name = "NewCreditCardNetworkPost")]
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
        [Route("CreditCardNetwork/Edit/{id}")]
        [Route("Edit/CreditCardNetwork/{id}", Name = "EditCreditCardNetwork")]
        public ActionResult Edit(String id)
        {
            Guid networkId = Guid.Empty;
            if (Guid.TryParse(id, out networkId))
            {
                ViewBag.ImageList = this.GetImageList();
                return View(ccnetworkDAO.GetById(networkId));
            }
            else
            {
                TempData["network_not_found"] = id;
                return RedirectToRoute("ListCreditCardNetworks");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("CreditCardNetwork/Edit")]
        [Route("Edit/CreditCardNetwork", Name = "EditCreditCardNetworkPost")]
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