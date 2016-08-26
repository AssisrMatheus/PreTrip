namespace PreTrip.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pretrip.pessoas")]
    public partial class pessoas
    {
        public pessoas()
        {
            usuarios = new HashSet<usuarios>();
        }

        [Key]
        public int ID_PESSOA { get; set; }

        [Required]
        [StringLength(255)]
        public string NM_PESSOA { get; set; }

        public int TELEFONE { get; set; }

        public int? CPF { get; set; }

        [StringLength(45)]
        public string DATA_NASC { get; set; }

        public int? ID_CONTA { get; set; }

        public virtual contas contas { get; set; }

        public virtual ICollection<usuarios> usuarios { get; set; }
    }
}
