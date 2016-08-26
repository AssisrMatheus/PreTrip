namespace PreTrip.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pretrip.contas")]
    public partial class contas
    {
        public contas()
        {
            pessoas = new HashSet<pessoas>();
        }

        [Key]
        public int ID_CONTA { get; set; }

        public double SALDO { get; set; }

        public virtual ICollection<pessoas> pessoas { get; set; }
    }
}
