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
    }
}
