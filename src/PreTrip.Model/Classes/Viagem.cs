using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreTrip.Model.Classes
{
    public class Viagem
    {
        public Viagem()
        {
            this.Pessoa = new Pessoa();
        }
        
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

        public virtual Pessoa Pessoa { get; set; }

        public virtual ICollection<Evento> Eventos { get; set; }

        public virtual ICollection<Avaliacao> Avaliacoes { get; set; }
        
    }

    public class ViagemMap : EntityTypeConfiguration<Viagem>
    {
        public ViagemMap()
        {
            //Nome da tabela
            ToTable("viagem");

            //Primary Key
            HasKey(x => x.Id);

            //Foreign Keys
            HasRequired(x => x.Origem);
            HasRequired(x => x.Destino);
            HasRequired(x => x.Empresa);
            HasRequired(x => x.Veiculo);
            HasRequired(x => x.Pessoa);

            //Many to one
            HasMany(x => x.Eventos);
            HasMany(x => x.Avaliacoes);
        }
    }
}
