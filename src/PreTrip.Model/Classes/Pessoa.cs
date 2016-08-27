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
        public Pessoa()
        {
            this.Conta = new Conta();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public int Telefone { get; set; }

        [Required]
        public int Cpf { get; set; }

        [Required]
        public DateTime DtNascimento { get; set; }

        public Conta Conta { get; set; }
    }
}
