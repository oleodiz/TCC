using Model;
using Persistencia.Manter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc5Project.Controllers
{
    public class AtividadeController : Controller
    {

        ManterAtividade mAtividade = new ManterAtividade();
        ManterPrioridadeAtividade mPrioridade = new ManterPrioridadeAtividade();
        ManterStatusAtividade mStatus = new ManterStatusAtividade();
        ManterProjeto mProjeto = new ManterProjeto();
        ManterProjetoUsuarioFuncao mPuf = new ManterProjetoUsuarioFuncao();
        ManterAcoesProjeto mAcoes = new ManterAcoesProjeto();
        public ActionResult Nova(int id)
        {

            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            string idUser = AccountController.FindIdByUser(User.Identity.Name);
            if (!mPuf.usuarioEstaNoProjeto(idUser, id))
                return RedirectToAction("Index", "Projetos");

            List<SelectListItem> comboBoxPrioridade = carregaPrioridadeAtividades();

            tb_projeto projeto = mProjeto.obterProId(id);
            List<string> participantes = new List<string>();
            List<string> ids = new List<string>();
            foreach (tb_projetoUsuarioFuncao usu in projeto.tb_projetoUsuarioFuncao)
            {
                participantes.Add(AccountController.FindUserNameById(usu.id_usuario));
                ids.Add(usu.id_usuario);
            }

            ViewBag.id_prioridadeAtividade = comboBoxPrioridade;
            ViewBag.Participantes = participantes;
            ViewBag.Ids = ids;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Nova(int id,tb_atividade model)
        {
            //Request.Form
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            string idUser = AccountController.FindIdByUser(User.Identity.Name);
            if (!mPuf.usuarioEstaNoProjeto(idUser, id))
                return RedirectToAction("Index", "Projetos");

            model.data_criacao = DateTime.Now;
            model.id_statusAtividade = 1;
            //model.id_atividade = id;
            model.id_usuario = idUser;
            model.id_projeto = id;

          
            if (ModelState.IsValid)
            {
                int idAtividade = mAtividade.salvarAtividade(model);

                if (idAtividade != -1)
                {
                    mAcoes.registraNovaAtividade(id, idUser, idAtividade, "Atividade '"+model.titulo+"' criada");
                    return RedirectToAction("P", "Projetos", new { id = id });
                }
            }

            
            List<SelectListItem> comboBoxPrioridade = carregaPrioridadeAtividades();
            
            tb_projeto projeto = mProjeto.obterProId(id);
            List<string> participantes = new List<string>();
            List<string> ids = new List<string>();
            foreach (tb_projetoUsuarioFuncao usu in projeto.tb_projetoUsuarioFuncao)
            {
                participantes.Add(AccountController.FindUserNameById(usu.id_usuario));
                ids.Add(usu.id_usuario);
            }

            ViewBag.id_prioridadeAtividade = comboBoxPrioridade;
            ViewBag.Participantes = participantes;
            ViewBag.Ids = ids;

            return View();
        }

        private List<SelectListItem> carregaStatusAtividade()
        {
            List<SelectListItem> comboBoxStatus = new List<SelectListItem>();
            List<tb_statusAtividade> status = mStatus.obterTodos();

            for (int i = 0; i < status.Count; i++)
            {
                comboBoxStatus.Add(new SelectListItem { Text = status[i].descricao, Value = status[i].id_statusAtividade.ToString() });
            }

            return comboBoxStatus;
        }
        private List<SelectListItem> carregaPrioridadeAtividades()
        {
            List<SelectListItem> comboBoxPrioridade= new List<SelectListItem>();
            List<tb_prioridadeAtividade> prioridade = mPrioridade.obterTodos();

            for (int i = 0; i < prioridade.Count; i++)
            {
                comboBoxPrioridade.Add(new SelectListItem { Text = prioridade[i].descricao, Value = prioridade[i].id_prioridadeAtividade.ToString() });
            }

            return comboBoxPrioridade;
        }
    }
}