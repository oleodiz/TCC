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
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class tb_etapa
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_etapa()
        {
            this.tb_atividade = new HashSet<tb_atividade>();
        }
    
        public int id_etapa { get; set; }
        public int id_projeto { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "O t�tulo deve conter entre {2} e {1} caracteres.", MinimumLength = 5)]
        [Display(Name = "T�tulo:")]
        public string titulo { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(8000, ErrorMessage = "Limite de caracteres excedido. M�ximo 8000 caracteres!", MinimumLength = 0)]
        [Display(Name = "Descri��o:")]
        public string descricao { get; set; }
        public int sequencia { get; set; }
        [Required]
        [Column(TypeName = "DateTime2")]
        [Display(Name = "In�cio:")]
        public System.DateTime data_inicio { get; set; }
        [Required]
        [Column(TypeName = "DateTime2")]
        [Display(Name = "Fim:")]
        public System.DateTime data_fim { get; set; }
        public int id_statusEtapa { get; set; }
        public string id_usuario { get; set; }
    
        public virtual tb_projeto tb_projeto { get; set; }
        public virtual tb_statusEtapa tb_statusEtapa { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_atividade> tb_atividade { get; set; }
    }
}
