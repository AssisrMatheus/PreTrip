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
            Usuario = new Usuario();
        }

        [Key]
        public int Id { get; set; }
        
        public string Cidade { get; set; }

        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }

        public Usuario Usuario { get; set; }
    }
}
