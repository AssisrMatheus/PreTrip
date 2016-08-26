using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreTrip.Model.Classes
{
    [Table("Pessoa")]
    public class Pessoa
    {
        [Key]
        public int Id { get; set; }

        public string Nome { get; set; }

        public int Telefone { get; set; }

        public int Cpf { get; set; }

        public DateTime DtNascimento { get; set; }

        public Conta Conta { get; set; }
    }
}
