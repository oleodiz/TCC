//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class tb_acoesProjeto
    {
        public int id_acao { get; set; }
        public int id_projeto { get; set; }
        public string id_usuario { get; set; }
        public int id_tipoAcaoProjeto { get; set; }
        public System.DateTime data_acao { get; set; }
        public string descricao { get; set; }
        public Nullable<int> id_atividade { get; set; }
        public Nullable<int> id_comentarioAtividade { get; set; }
        public Nullable<int> id_etapa { get; set; }
        public string username { get; set; }
        public string nomeProjeto { get; set; }

        public virtual tb_projeto tb_projeto { get; set; }
        public virtual tb_tipoAcaoProjeto tb_tipoAcaoProjeto { get; set; }
        public virtual tb_atividade tb_atividade { get; set; }
        public virtual tb_comentarioAtividade tb_comentarioAtividade { get; set; }
        public virtual tb_etapa tb_etapa { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
    }
}
