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
    
    public partial class tb_projetoUsuarioFuncao
    {
        public int id_projeto { get; set; }
        public string id_usuario { get; set; }
        public int id_funcaoProjeto { get; set; }
        public DateTime data_inclusao { get { return DateTime.Now; } set {} }
        public string username { get; set; }
        public string funcao { get; set; }
        public virtual tb_funcaoProjeto tb_funcaoProjeto { get; set; }
        public virtual tb_projeto tb_projeto { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
    }
}
