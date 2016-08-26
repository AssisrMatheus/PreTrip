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

        public Viagem Viagem { get; set; }
    }
}
