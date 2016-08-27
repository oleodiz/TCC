using Model;
using Persistencia.Manter;
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

        ManterFuncao mFuncao = new ManterFuncao();
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


            List<SelectListItem> comboBoxFuncoes = new List<SelectListItem>();
            List<tb_funcaoProjeto> funcoes = mFuncao.obterTodos();

            for (int i =0; i < funcoes.Count; i++)
            {
                comboBoxFuncoes.Add(new SelectListItem { Text = funcoes[i].descricao, Value = funcoes[i].id_funcaoProjeto.ToString() });
            }

            ViewBag.ComboFuncoes = comboBoxFuncoes;


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