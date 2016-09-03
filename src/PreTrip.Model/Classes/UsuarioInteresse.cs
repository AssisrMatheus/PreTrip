using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreTrip.Model.Classes
{
    [Table("UsuarioInteresse")]
    public class UsuarioInteresse
    {
        public UsuarioInteresse()
        {
            Usuario = new Usuario();
        }

        [Key]
        public int Id { get; set; }
        
        public string Cidade { get; set; }
        
        public virtual Usuario Usuario { get; set; }
    }
}
