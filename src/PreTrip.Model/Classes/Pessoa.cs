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
            this.ContaBancaria = new ContaBancaria();
            this.Interesses = new List<Interesse>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public long Telefone { get; set; }

        [Required]
        [RegularExpression("^[0-9]{11}$")]
        public string Cpf { get; set; }

        [Required]
        public DateTime DtNascimento { get; set; }

        public string UrlImagem { get; set; }

        [ForeignKey("ContaBancaria")]
        public int ContaBancariaId { get; set; }

        public virtual ContaBancaria ContaBancaria { get; set; }

        public virtual IEnumerable<Interesse> Interesses { get; set; }

        public virtual IEnumerable<Viagem> Viagens { get; set; }

        public virtual IEnumerable<Pedido> Pedidos { get; set; }
    }
}
