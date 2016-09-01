using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreTrip.Model.Classes
{
    [Table("Busca")]
    public class Busca
    {
        public int Id { get; set; }
        public string Termo { get; set; }
    }
}
