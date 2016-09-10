using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Manter
{
    public class ManterProjetoUsuarioFuncao
    {

        tccEntities conexao;
        public ManterProjetoUsuarioFuncao()
        {
            conexao = new tccEntities();
        }

        public int adicionarUsuario(tb_projetoUsuarioFuncao puf)
        {
            try
            {
                conexao.tb_projetoUsuarioFuncao.Add(puf);
                conexao.SaveChanges();
                return puf.id_projeto;
            }
            catch (Exception dbEx)
            {
                return -1;
            }

        }
        public bool usuarioIsOrientador(string idUser, int idProjeto)
        {
            tb_projetoUsuarioFuncao puf = conexao.tb_projetoUsuarioFuncao.Where(p => p.id_projeto == idProjeto && p.id_usuario == idUser).FirstOrDefault();
            if (puf != null && puf.id_funcaoProjeto == 2)
                return true;
            else
                return false;
        }

        public bool usuarioJaEstaNoProjeto(string idUser, int idProjeto)
        {
            tb_projetoUsuarioFuncao puf = conexao.tb_projetoUsuarioFuncao.Where(p => p.id_projeto == idProjeto && p.id_usuario == idUser).FirstOrDefault();
            if (puf != null)
                return true;
            else
                return false;
        }

    }
}
