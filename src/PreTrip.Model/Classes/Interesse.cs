using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreTrip.Model.Classes
{
    [Table("Interesse")]
    public class Interesse
    {
        public Interesse()
        {
            Pessoa = new Pessoa();
        }

        [Key]
        public int Id { get; set; }
        
        public string Cidade { get; set; }

        [ForeignKey("Pessoa")]
        public int PessoaId { get; set; }

        public virtual Pessoa Pessoa { get; set; }
    }
}
