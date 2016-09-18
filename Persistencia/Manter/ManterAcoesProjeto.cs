using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Manter
{
    public class ManterAcoesProjeto
    {
        tccEntities conexao;
        public ManterAcoesProjeto()
        {
            conexao = new tccEntities();
        }

        public bool registraNovaAtividade(int id_projeto, string id_usuario, string username, int id_atividade, string descricao)
        {
            tb_acoesProjeto acao = new tb_acoesProjeto();
            acao.data_acao = DateTime.Now;
            acao.descricao = descricao;
            acao.id_projeto = id_projeto;
            acao.id_usuario = id_usuario;
            acao.id_tipoAcaoProjeto = 7; //Id de nova atividade
            acao.id_atividade = id_atividade;
            acao.username = username;
            try
            {
                conexao.tb_acoesProjeto.Add(acao);
                conexao.SaveChanges();
                return true;
            }
            catch (Exception dbEx)
            {
                return false;
            }
        }

        public List<tb_acoesProjeto> obterAcoesDosProjetosDoUsuario(string id_usuario, List<int> idsProjetos, DateTime ultimaData)
        {
            //TODO Colocar no where o status do projeto
            List<tb_acoesProjeto> acoes = conexao.tb_acoesProjeto.Where(a => idsProjetos.Contains(a.id_projeto) && a.data_acao <= ultimaData).OrderByDescending(a => a.data_acao).ToList();

            return acoes;
        }

    }
}
