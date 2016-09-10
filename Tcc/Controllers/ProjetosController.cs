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
        ManterProjeto mProjeto = new ManterProjeto();
        ManterEtapa mEtapa = new ManterEtapa();

        // GET: Projetos       
        public ActionResult Index()
        {

            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            string idUser = AccountController.FindIdByUser(User.Identity.Name);
            ViewBag.Projetos = mProjeto.obterProjetosDoUsuario(idUser);

            return View();
        }


        // GET: Projetos       
        public ActionResult P(int id)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            string idUser = AccountController.FindIdByUser(User.Identity.Name);
            ViewBag.Projetos = mProjeto.obterProjetosDoUsuario(idUser);

            tb_projeto projeto = mProjeto.obterProId(id);

            List<string> participantes = new List<string>();
            foreach(tb_projetoUsuarioFuncao usu in projeto.tb_projetoUsuarioFuncao)
            {
                participantes.Add(AccountController.FindUserNameById(usu.id_usuario));
            }

            ViewBag.Participantes  = participantes;
            ViewBag.Informacao = projeto.titulo;
            return View(projeto);
        }

        [HttpGet]
        public ActionResult Novo()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            string idUser = AccountController.FindIdByUser(User.Identity.Name);
            ViewBag.Projetos = mProjeto.obterProjetosDoUsuario(idUser);

            AccountController.membros = new List<tb_projetoUsuarioFuncao>();
            tb_projetoUsuarioFuncao dono = new tb_projetoUsuarioFuncao();
            dono.id_funcaoProjeto = 2;
            dono.id_usuario = AccountController.FindIdByUser(User.Identity.Name);
            dono.username = User.Identity.Name;
            dono.funcao = "Orientador";
            AccountController.membros.Add(dono);
            List<SelectListItem> comboBoxFuncoes = carregaFuncoes();

            ViewBag.ComboFuncoes = comboBoxFuncoes;

            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Novo(tb_projeto model)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");


            if (ModelState.IsValid)
            {
                model.id_usuario = AccountController.FindIdByUser(User.Identity.Name);
                model.id_statusProjeto = 1;

            
                int id = mProjeto.salvarProjeto(model);
                if (id != -1)
                    return RedirectToAction("P", "Projetos", new { id = id });
            }
            string idUser = AccountController.FindIdByUser(User.Identity.Name);
            ViewBag.Projetos = mProjeto.obterProjetosDoUsuario(idUser);

            List<SelectListItem> comboBoxFuncoes = carregaFuncoes();
            ViewBag.ComboFuncoes = comboBoxFuncoes;

            return View();
        }

        // GET: Projetos       
        public ActionResult NovaEtapa(int id)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            string idUser = AccountController.FindIdByUser(User.Identity.Name);
          
            tb_projeto projeto = mProjeto.obterProId(id);

            ViewBag.Informacao = projeto.titulo;
            ViewBag.Id_projeto = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NovaEtapa(tb_etapa model)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            
            if (ModelState.IsValid)
            {
                model.id_usuario = AccountController.FindIdByUser(User.Identity.Name);
                model.id_statusEtapa = 1;
                model.sequencia = 1;
                

                int id = mEtapa.salvarEtapa(model);
                if (id != -1)
                    return RedirectToAction("P", "Projetos", new { id = model.id_projeto });
            }
            string idUser = AccountController.FindIdByUser(User.Identity.Name);

            tb_projeto projeto = mProjeto.obterProId(model.id_projeto);

            ViewBag.Informacao = projeto.titulo;
            ViewBag.Id_projeto = model.id_projeto;

            return View();
        }

        private List<SelectListItem> carregaFuncoes()
        {
            List<SelectListItem> comboBoxFuncoes = new List<SelectListItem>();
            List<tb_funcaoProjeto> funcoes = mFuncao.obterTodos();

            for (int i = 0; i < funcoes.Count; i++)
            {
                comboBoxFuncoes.Add(new SelectListItem { Text = funcoes[i].descricao, Value = funcoes[i].id_funcaoProjeto.ToString() });
            }

            return comboBoxFuncoes;
        }

        
    }
}