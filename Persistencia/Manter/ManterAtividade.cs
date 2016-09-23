using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Manter
{
    public class ManterAtividade
    {

        tccEntities conexao;
        public ManterAtividade()
        {
            conexao = new tccEntities();
        }

        public int salvarAtividade(tb_atividade atividade)
        {
            try
            {
                conexao.tb_atividade.Add(atividade);
                conexao.SaveChanges();
                return atividade.id_atividade;
            }
            catch (Exception dbEx)
            {
                return -1;
            }

        }

        public tb_atividade obterProId(int idAtividade)
        {
            tb_atividade atividade = conexao.tb_atividade.ToList().Where(p => p.id_atividade == idAtividade).FirstOrDefault();
            return atividade;
        }

        public List<tb_atividade> obterAtividadesDoUsuario(int idProjeto, string idUsuario)
        {
            List<tb_atividade> atividades = conexao.tb_atividade.Where(p => p.id_projeto == idProjeto && p.tb_atividadeResponsavel.Any(f => f.id_usuario == idUsuario)).ToList();
            return atividades;
        }

        public List<tb_atividade> obterAtividadesDoProjeto(int idProjeto)
        {
            List<tb_atividade> atividades = conexao.tb_atividade.Where(p => p.id_projeto == idProjeto).ToList();
            return atividades;
        }

        public int obterUltimoNumeroSequencia(int idProjeto)
        {
            tb_atividade atividades = conexao.tb_atividade.Where(p => p.id_projeto == idProjeto).ToList().Last();
            if (atividades == null)
                return 1;

            return atividades.sequencia+1;
        }
    }
}
