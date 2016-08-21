using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Mvc5Project.Controllers
{
    public class ProjetosController : Controller
    {
        // GET: Projetos       
        public ActionResult Index()
        {

            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }


        [HttpGet]
        public ActionResult Novo()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Novo(string id, tb_projeto model)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");


            return View();
        }
    }
}