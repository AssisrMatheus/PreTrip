namespace PreTrip.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pretrip.eventos")]
    public partial class eventos
    {
        public eventos()
        {
            viagens = new HashSet<viagens>();
        }

        [Key]
        public int ID_EVENTO { get; set; }

        [StringLength(130)]
        public string NM_EVENTO { get; set; }

        [StringLength(130)]
        public string TAGS { get; set; }

        public virtual ICollection<viagens> viagens { get; set; }
    }
}
