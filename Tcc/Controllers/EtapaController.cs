using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc5Project.Controllers
{
    public class EtapaController : Controller
    {

        public ActionResult Index()
        {

            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            string idUser = AccountController.FindIdByUser(User.Identity.Name);

            return View();
        }
    }
}