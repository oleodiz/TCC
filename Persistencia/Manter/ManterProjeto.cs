using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Manter
{
    public class ManterProjeto
    {
        tccEntities conexao;
        public ManterProjeto()
        {
            conexao = new tccEntities();
        }

        public int salvarProjeto(tb_projeto projeto)
        {
            try
            {
                conexao.tb_projeto.Add(projeto);
                conexao.SaveChanges();
                return projeto.id_projeto;
            }
            catch (Exception dbEx)
            {
                return -1;
            }

        }

        public tb_projeto obterProId(int idProjeto)
        {
            tb_projeto projeto = conexao.tb_projeto.ToList().Where(p => p.id_projeto == idProjeto).FirstOrDefault();
            return projeto;
        }

        public List<tb_projeto> obterProjetosDoUsuario(string idUsuario)
        {
            List<tb_projeto> projetos = conexao.tb_projeto.Where(p => p.tb_projetoUsuarioFuncao.Any(f => f.id_usuario == idUsuario)).ToList();
            return projetos;
        }

       
    }
}
