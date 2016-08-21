using Model;
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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            using (var context = new tccEntities())
            {
                tb_projeto proj = new tb_projeto() {id_projeto= 1, titulo = "TESTE", data_inicio = DateTime.Now, id_status = 1, descricao = "OUTRO TESTE", data_fim = DateTime.Now };

                context.tb_projeto.Add(proj);
                context.SaveChanges();
            }

           
          

            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}