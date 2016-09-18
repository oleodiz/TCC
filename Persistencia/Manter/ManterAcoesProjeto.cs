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

        public bool registraNovaAtividade(int id_projeto, string id_usuario, int id_atividade, string descricao)
        {
            tb_acoesProjeto acao = new tb_acoesProjeto();
            acao.data_acao = DateTime.Now;
            acao.descricao = descricao;
            acao.id_projeto = id_projeto;
            acao.id_usuario = id_usuario;
            acao.id_tipoAcaoProjeto = 8; //Id de nova atividade
            acao.id_atividade = id_atividade;
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

    }
}
