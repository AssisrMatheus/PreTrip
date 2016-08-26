using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreTrip.Model.Classes
{
    [Table("Veiculo")]
    public class Veiculo
    {
        [Key]
        public int Id { get; set; }

        public string Modelo { get; set; }

        public string Placa { get; set; }

        public string Tipo { get; set; }

        public int QuantidadeLugares { get; set; }
    }
}
