namespace PreTrip.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pretrip.veiculos")]
    public partial class veiculos
    {
        public veiculos()
        {
            viagens = new HashSet<viagens>();
        }

        [Key]
        public int ID_VEICULO { get; set; }

        [StringLength(255)]
        public string MODELO { get; set; }

        [StringLength(8)]
        public string PLACA { get; set; }

        [StringLength(100)]
        public string TIPO { get; set; }

        public int? QTD_LUGARES { get; set; }

        public virtual ICollection<viagens> viagens { get; set; }
    }
}
