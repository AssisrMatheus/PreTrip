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
        [Key]
        public int Id { get; set; }

        public string Login { get; set; }

        public string Senha { get; set; }

        public bool IsAdmin { get; set; }

        public Pessoa Pessoa { get; set; }

        public List<Pedido> Pedidos { get; set; }
    }
}
