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
    
    public partial class tb_comentarioAtividade
    {
        public int id_comentario { get; set; }
        public int id_atividade { get; set; }
        public string id_usuario { get; set; }
        public string comentario { get; set; }
        public System.DateTime data_comentario { get; set; }
        public Nullable<bool> ativo { get; set; }
    
        public virtual tb_atividade tb_atividade { get; set; }
    }
}