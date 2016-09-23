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
        ManterProjetoUsuarioFuncao mPuf = new ManterProjetoUsuarioFuncao();
        ManterAcoesProjeto mAcoes = new ManterAcoesProjeto();
        ManterAtividade mAtividade = new ManterAtividade();
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
            bool isOrientador = false;
            tb_projeto projeto = mProjeto.obterProId(id);
            List<SelectListItem> comboBoxFuncoes = carregaFuncoes();

            List<string> participantes = new List<string>();
            List<string> funcoes = new List<string>();
            foreach (tb_projetoUsuarioFuncao usu in projeto.tb_projetoUsuarioFuncao)
            {
                participantes.Add(AccountController.FindUserNameById(usu.id_usuario));
                if (usu.id_usuario == idUser && usu.id_funcaoProjeto == 2)
                    isOrientador = true;
                
                foreach (SelectListItem item in comboBoxFuncoes)
                {
                    if (item.Value == usu.id_funcaoProjeto + "")
                        funcoes.Add(item.Text);
                }

            }



            ViewBag.Atividades = mAtividade.obterAtividadesDoProjeto(id);
            ViewBag.ComboFuncoes = comboBoxFuncoes;
            ViewBag.Funcoes = funcoes;
            ViewBag.Participantes  = participantes;
            ViewBag.Informacao = projeto.titulo;
            ViewBag.IsOrientador = isOrientador;
            ViewBag.IdProjeto = id;
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

        [HttpGet]
        public JsonResult AdicionarUsuarioAoProjeto(string userEmail, int idFuncao, int idProjeto)
        {
            if (!User.Identity.IsAuthenticated)
                return Json("Você não está autenticado!", JsonRequestBehavior.AllowGet);

            string IdOrientador = AccountController.FindIdByUser(User.Identity.Name); //Verificar se o usuário que está tentando adicionar outro ao projeto é Orientador
            if (!mPuf.usuarioIsOrientador(IdOrientador, idProjeto))
                return Json("Você não tem permissão!", JsonRequestBehavior.AllowGet);
           

            if (userEmail.Contains("@"))
                userEmail = AccountController.FindIdByEmail(userEmail);
            else
                userEmail = AccountController.FindIdByUser(userEmail);

            if (mPuf.usuarioEstaNoProjeto(userEmail, idProjeto))
                return Json("Este usuário já é um membro do projeto!", JsonRequestBehavior.AllowGet);

            if (userEmail == null || userEmail == "")
                return Json("Usuário não encontrado", JsonRequestBehavior.AllowGet);

            tb_projetoUsuarioFuncao puf = new tb_projetoUsuarioFuncao();
            puf.data_inclusao = DateTime.Now;
            puf.id_funcaoProjeto = idFuncao;
            puf.id_usuario = userEmail;
            puf.id_projeto = idProjeto;

            if (mPuf.adicionarUsuario(puf) != -1)
            {
                return Json("Usuário adicionado!", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Problema ao adicionar Usuário!", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult ListaAcoes(String ultimaData)
        {
            if (ultimaData == null)
                return null;
            //TODO VERIFICAR SE ESSE METODO ESTÁ DEMORANDO
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            string idUser = AccountController.FindIdByUser(User.Identity.Name);

            List<int> idsProjetoUsuario = new List<int>();
            List<tb_projeto> projetos = mProjeto.obterProjetosDoUsuario(idUser);

            for (int i=0; i < projetos.Count; i++)
            {
                idsProjetoUsuario.Add(projetos[i].id_projeto);
            }

            List<tb_acoesProjeto> acoes = mAcoes.obterAcoesDosProjetosDoUsuario(idUser, idsProjetoUsuario, DateTime.Parse( ultimaData));
            if (acoes != null)
            for (int i =0; i < acoes.Count; i++) //Preguiça de fazer um Join
            {
                foreach(tb_projeto p in projetos)
                {
                    if (acoes[i].id_projeto == p.id_projeto)
                        acoes[i].nomeProjeto = p.titulo;
                }
            }

            return PartialView(acoes);
        }


    }
}