namespace PreTrip.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pretrip.empresas")]
    public partial class empresas
    {
        public empresas()
        {
            viagens = new HashSet<viagens>();
        }

        [Key]
        public int ID_EMPRESA { get; set; }

        public int? ID_ENDERECO { get; set; }

        [Column(TypeName = "char")]
        [StringLength(14)]
        public string CNPJ { get; set; }

        [StringLength(255)]
        public string RAZAO_SOCIAL { get; set; }

        [StringLength(255)]
        public string NM_FANTASIA { get; set; }

        public virtual ICollection<viagens> viagens { get; set; }

        public virtual enderecos enderecos { get; set; }
    }
}
