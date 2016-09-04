using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreTrip.Model.Classes
{
    [Table("Busca")]
    public class Busca
    {             
        public Busca()
        {
            DtHrBusca = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        public string Titulo { get; set; }
        public double Preco { get; set; }
        public int QuantidadeLugares { get; set; }
        public int LugaresDisponiveis { get; set; }
        public string Origem { get; set; }
        public string Destino { get; set; }

        public DateTime DtHrBusca { get; set; }

        public Usuario Usuario { get; set; }

        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }
    }
}
