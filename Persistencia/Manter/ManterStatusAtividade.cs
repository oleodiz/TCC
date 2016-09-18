using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Manter
{
    public class ManterStatusAtividade
    {
        tccEntities conexao;
        public ManterStatusAtividade()
        {
            conexao = new tccEntities();
        }


        public List<tb_statusAtividade> obterTodos()
        {
            List<tb_statusAtividade> status = conexao.tb_statusAtividade.ToList();
            return status;
        }
        public tb_statusAtividade obterProId(int idStatusAtividade)
        {
            tb_statusAtividade status = conexao.tb_statusAtividade.ToList().Where(f => f.id_statusAtividade == idStatusAtividade).FirstOrDefault();
            return status;
        }
    }
}
