using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Manter
{
    public class ManterProjeto
    {
        tccEntities conexao;
        public ManterProjeto()
        {
            conexao = new tccEntities();
        }

        public void salvarProjeto(tb_projeto projeto)
        {
            try
            {
                conexao.tb_projeto.Add(projeto);
                conexao.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
            }

        }
    }
}
