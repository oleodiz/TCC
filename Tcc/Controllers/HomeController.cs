using Model;
using Persistencia.Manter;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc5Project.Controllers
{
    public class HomeController : Controller
    {
        ManterProjeto mProjeto = new ManterProjeto();
        public ActionResult Index()
        {

            if (User.Identity.IsAuthenticated)
            { 
                string idUser = AccountController.FindIdByUser(User.Identity.Name);
                ViewBag.Projetos = mProjeto.obterProjetosDoUsuario(idUser);
            }

            return View();
        }

        public ActionResult About()
        {
            if (User.Identity.IsAuthenticated)
            {
                string idUser = AccountController.FindIdByUser(User.Identity.Name);
                ViewBag.Projetos = mProjeto.obterProjetosDoUsuario(idUser);
            }

            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            if (User.Identity.IsAuthenticated)
            {
                string idUser = AccountController.FindIdByUser(User.Identity.Name);
                ViewBag.Projetos = mProjeto.obterProjetosDoUsuario(idUser);
            }

            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}