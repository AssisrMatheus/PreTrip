using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreTrip.Model.Classes
{
    [Table("Usuario")]
    public class Usuario
    {
        public Usuario()
        {
            this.Pessoa = new Pessoa();
            this.Pedidos = new List<Pedido>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Senha { get; set; }

        public string Email { get; set; }

        public bool IsAdmin { get; set; }

        public Pessoa Pessoa { get; set; }

        public virtual IEnumerable<Pedido> Pedidos { get; set; }
    }
}
