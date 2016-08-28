using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Manter
{
    public class ManterFuncao
    {
        tccEntities conexao;
        public ManterFuncao()
        {
            conexao = new tccEntities();
        }


        public List<tb_funcaoProjeto> obterTodos()
        {
            List<tb_funcaoProjeto> funcoes =  conexao.tb_funcaoProjeto.ToList();
            return funcoes;
        }
        public tb_funcaoProjeto obterProId(int idFuncao)
        {
            tb_funcaoProjeto funcao = conexao.tb_funcaoProjeto.ToList().Where(f => f.id_funcaoProjeto == idFuncao).FirstOrDefault();
            return funcao;
        }
    }
}
