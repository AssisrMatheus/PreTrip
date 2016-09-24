using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreTrip.Model.Classes
{
    [Table("Cidade")]
    public class Cidade
    {
        [Key]
        public int IdCidade { get; set; }
        public string Nome { get; set; }      
   }
}
