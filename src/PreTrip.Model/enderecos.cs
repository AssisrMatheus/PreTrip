namespace PreTrip.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pretrip.enderecos")]
    public partial class enderecos
    {
        public enderecos()
        {
            empresas = new HashSet<empresas>();
        }

        [Key]
        public int ID_ENDERECO { get; set; }

        public int? NUM { get; set; }

        public int? COMPLEMENTO { get; set; }

        [StringLength(50)]
        public string RUA { get; set; }

        [StringLength(80)]
        public string BAIRRO { get; set; }

        [StringLength(30)]
        public string CIDADE { get; set; }

        [StringLength(40)]
        public string ESTADO { get; set; }

        public virtual ICollection<empresas> empresas { get; set; }
    }
}
