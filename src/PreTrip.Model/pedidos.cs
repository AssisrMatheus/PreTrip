namespace PreTrip.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pretrip.pedidos")]
    public partial class pedidos
    {
        public pedidos()
        {
            usuarios = new HashSet<usuarios>();
        }

        [Key]
        public int ID_PEDIDO { get; set; }

        public int? ID_VIAGEM { get; set; }

        public DateTime? DT_REALIZACAO { get; set; }

        public int? QTD { get; set; }

        public double? PRECO_TOTAL { get; set; }

        public virtual viagens viagens { get; set; }

        public virtual ICollection<usuarios> usuarios { get; set; }
    }
}
