using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Manter
{
    public class ManterComentarios
    {
        tccEntities conexao;
        public ManterComentarios()
        {
            conexao = new tccEntities();
        }

        public int salvarComentario(tb_comentarioAtividade comentario)
        {
            try
            {
                conexao.tb_comentarioAtividade.Add(comentario);
                conexao.SaveChanges();
                return comentario.id_comentario;
            }
            catch (Exception dbEx)
            {
                return -1;
            }

        }

        public List<tb_comentarioAtividade> obterComentariosDaAtividade(int idAtividade)
        {
            List<tb_comentarioAtividade> comentarios = conexao.tb_comentarioAtividade.Where(c => c.id_atividade == idAtividade && (bool)c.ativo).ToList();
            return comentarios;
        }

        public tb_comentarioAtividade obterProId(int idComentario)
        {
            tb_comentarioAtividade atividade = conexao.tb_comentarioAtividade.Include("AspNetUser").Where(p => p.id_comentario == idComentario).FirstOrDefault();
            if (atividade == null)
                return null; //Teste

            return atividade;
        }

    }
}
