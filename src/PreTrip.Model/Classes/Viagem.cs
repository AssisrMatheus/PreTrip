using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreTrip.Model.Classes
{
    [Table("Viagem")]
    public class Viagem
    {
        [Key]
        public int Id { get; set; }

        public double PrecoPassagem { get; set; }

        public string Descricao { get; set; }

        public DateTime DtHrSaida { get; set; }

        public DateTime DtHrChegadaEstimada { get; set; }

        public int QuantidadeLugaresDisponiveis { get; set; }

        public Endereco Origem { get; set; }

        public Endereco Destino { get; set; }

        public Empresa Empresa { get; set; }

        public Veiculo Veiculo { get; set; }

        public List<Evento> Eventos { get; set; }
    }
}
