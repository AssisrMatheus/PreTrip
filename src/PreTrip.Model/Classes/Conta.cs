using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreTrip.Model.Classes
{
    [Table("Conta")]
    public class Conta
    {
        [Key]
        public int Id { get; set; }

        public double Saldo { get; set; }
    }
}
