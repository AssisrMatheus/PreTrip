using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreTrip.Model.Classes
{
    [Table("Avaliacao")]
    public class Avaliacao
    {
        [Key]
        public int Id { get; set; }

        public int Nota { get; set; }

        public string Comentario { get; set; }

        public Usuario Usuario { get; set; }

        [ForeignKey("Viagem")]
        public int ViagemId { get; set; }
        
        public Viagem Viagem { get; set; }
    }
}
