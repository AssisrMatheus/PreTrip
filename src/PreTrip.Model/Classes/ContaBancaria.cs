using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreTrip.Model.Classes
{
    [Table("ContaBancaria")]
    public class ContaBancaria
    {
        [Key]
        public int Id { get; set; }

        public double Saldo { get; set; }

        public int Numero { get; set; }       
    }
}
