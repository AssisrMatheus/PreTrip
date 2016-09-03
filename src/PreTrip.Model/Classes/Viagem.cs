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

        public string Titulo { get; set; }

        public string UrlImagem { get; set; }

        public double PrecoPassagem { get; set; }

        public string Descricao { get; set; }

        public DateTime DtHrSaida { get; set; }

        public DateTime DtHrChegadaEstimada { get; set; }

        public int QuantidadeLugaresDisponiveis { get; set; }

        public Endereco Origem { get; set; }

        public Endereco Destino { get; set; }

        public Empresa Empresa { get; set; }

        public Veiculo Veiculo { get; set; }

        public IEnumerable<Evento> Eventos { get; set; }

        public IEnumerable<Avaliacao> Avaliacoes { get; set; }
    }
}
