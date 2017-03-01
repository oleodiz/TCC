using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Manter
{
    public class ManterEtapa
    {
        tccEntities conexao;
        public ManterEtapa()
        {
            conexao = new tccEntities();
        }


        public List<tb_etapa> obterTodosPorProjeto(int idProjeto)
        {
            List<tb_etapa> etapas = conexao.tb_etapa.Where(e => e.id_projeto == idProjeto).ToList();
            return etapas;
        }
        public tb_atividade obterAtividadeProId(int idAtividade)
        {
            tb_atividade atividade = conexao.tb_atividade.ToList().Where(p => p.id_atividade == idAtividade).FirstOrDefault();
            return atividade;
        }
        public int salvarEtapa(tb_etapa etapa)
        {
            try
            {
                conexao.tb_etapa.Add(etapa);
                conexao.SaveChanges();
                return etapa.id_etapa;
            }
            catch (Exception dbEx)
            {
                return -1;
            }

        }
    }
}
