namespace PreTrip.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pretrip.usuarios")]
    public partial class usuarios
    {
        public usuarios()
        {
            pedidos = new HashSet<pedidos>();
            viagens = new HashSet<viagens>();
        }

        [Key]
        public int ID_USUARIO { get; set; }

        [Required]
        [StringLength(20)]
        public string LOGIN { get; set; }

        [StringLength(20)]
        public string SENHA { get; set; }

        public sbyte? IS_ADMIN { get; set; }

        public int? ID_PESSOA { get; set; }

        public virtual pessoas pessoas { get; set; }

        public virtual ICollection<pedidos> pedidos { get; set; }

        public virtual ICollection<viagens> viagens { get; set; }
    }
}
