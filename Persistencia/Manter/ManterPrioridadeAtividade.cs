using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Manter
{
    public class ManterPrioridadeAtividade
    {
        tccEntities conexao;
        public ManterPrioridadeAtividade()
        {
            conexao = new tccEntities();
        }


        public List<tb_prioridadeAtividade> obterTodos()
        {
            List<tb_prioridadeAtividade> priodidade = conexao.tb_prioridadeAtividade.ToList();
            return priodidade;
        }
        public tb_prioridadeAtividade obterProId(int idPrioridadeAtividade)
        {
            tb_prioridadeAtividade priodidades = conexao.tb_prioridadeAtividade.ToList().Where(f => f.id_prioridadeAtividade == idPrioridadeAtividade).FirstOrDefault();
            return priodidades;
        }
    }
}
