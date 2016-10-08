using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreTrip.Model.Classes
{
    [Table("Pedido")]
    public class Pedido
    {
        [Key]
        public int Id { get; set; }

        public int Quantidade { get; set; }

        public double PrecoFinal { get; set; }

        public DateTime DtHrRealizacao { get; set; }

        [ForeignKey("Viagem")]
        public int ViagemId { get; set; }
        
        public virtual Viagem Viagem { get; set; }

        public Pedido()
        {
            this.DtHrRealizacao = DateTime.Now;
            this.Quantidade = 1;
        }

        public Pedido(Viagem viagem)
        {
            this.DtHrRealizacao = DateTime.Now;
            this.Quantidade = 1;

            this.Viagem = viagem;
            this.ViagemId = viagem.Id;            
            this.PrecoFinal = viagem.PrecoPassagem;
        }
    }
}
