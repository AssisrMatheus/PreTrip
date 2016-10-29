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
    public class Avaliacao
    {
        public Avaliacao(Pessoa pessoa, Viagem viagem)
        {
            this.Pessoa = pessoa;
            this.Viagem = viagem;
        }

        public int Id { get; set; }

        public int PessoaId { get; set; }

        public int ViagemId { get; set; }

        public int Nota { get; set; }

        public string Comentario { get; set; }

        public virtual Pessoa Pessoa { get; set; }
        
        public virtual Viagem Viagem { get; set; }
    }

    public class AvaliacaoMap : EntityTypeConfiguration<Avaliacao>
    {
        public AvaliacaoMap()
        {
            //Nome da tabela
            ToTable("avaliacao");

            //Primary Key
            HasKey(x => x.Id);

            HasRequired(x => x.Pessoa).WithMany(x => x.Avaliacoes).HasForeignKey(x => x.PessoaId);

            HasRequired(x => x.Viagem).WithMany(x => x.Avaliacoes).HasForeignKey(x => x.ViagemId);
        }
    }
}
