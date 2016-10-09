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
        public Viagem()
        {
            this.Pessoa = new Pessoa();
        }

        [Key]
        public int Id { get; set; }

        public string Titulo { get; set; }

        public string UrlImagem { get; set; }

        public double PrecoPassagem { get; set; }

        public string Descricao { get; set; }

        public DateTime DtHrSaida { get; set; }

        public DateTime DtHrChegadaEstimada { get; set; }

        public int QuantidadeLugaresDisponiveis { get; set; }

        public virtual Endereco Origem { get; set; }

        public virtual Endereco Destino { get; set; }

        public virtual Empresa Empresa { get; set; }

        public virtual Veiculo Veiculo { get; set; }

        public virtual IEnumerable<Evento> Eventos { get; set; }

        public virtual IEnumerable<Avaliacao> Avaliacoes { get; set; }

        [ForeignKey("Pessoa")]
        public int PessoaId { get; set; }

        public virtual Pessoa Pessoa { get; set; }
    }
}
