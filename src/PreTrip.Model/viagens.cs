namespace PreTrip.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pretrip.viagens")]
    public partial class viagens
    {
        public viagens()
        {
            pedidos = new HashSet<pedidos>();
            usuarios = new HashSet<usuarios>();
            eventos = new HashSet<eventos>();
        }

        [Key]
        public int ID_VIAGEM { get; set; }

        public int? ID_VEICULO { get; set; }

        public int? ID_EMPRESA { get; set; }

        public double? PRECO_PASSAGEM { get; set; }

        [StringLength(45)]
        public string DS_VIAGEM { get; set; }

        public DateTime? DT_SAIDA { get; set; }

        public DateTime? DT_CHEGADA_ESPERADA { get; set; }

        public int? LUGARES_DISPONIVEIS { get; set; }

        public virtual empresas empresas { get; set; }

        public virtual ICollection<pedidos> pedidos { get; set; }

        public virtual veiculos veiculos { get; set; }

        public virtual ICollection<usuarios> usuarios { get; set; }

        public virtual ICollection<eventos> eventos { get; set; }
    }
}
