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
    public class Pessoa
    {
        public Pessoa()
        {
            this.Interesses = new List<Interesse>();
            this.Viagens = new List<Viagem>();
            this.Pedidos = new List<Pedido>();
            this.Avaliacoes = new List<Avaliacao>();
        }
        
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public long Telefone { get; set; }

        [Required]
        [RegularExpression("^[0-9]{11}$")]
        public string Cpf { get; set; }

        [Required]
        public DateTime DtNascimento { get; set; }

        public string UrlImagem { get; set; }
        
        public virtual ContaBancaria ContaBancaria { get; set; }

        public virtual Usuario Usuario { get; set; }

        public ICollection<Interesse> Interesses { get; set; }

        public ICollection<Viagem> Viagens { get; set; }

        public ICollection<Pedido> Pedidos { get; set; }

        public ICollection<Avaliacao> Avaliacoes { get; set; }
    }

    public class PessoaMap : EntityTypeConfiguration<Pessoa>
    {
        public PessoaMap()
        {
            //Nome da tabela
            ToTable("Pessoa");

            //Primary Key
            HasKey(x => x.Id);            

            Property(x => x.Nome).IsRequired();
            Property(x => x.Cpf).IsRequired();
            Property(x => x.DtNascimento).IsRequired();

            //Foreign Key
            HasRequired(x => x.Usuario).WithRequiredDependent(x => x.Pessoa);

            HasMany(x => x.Interesses);
            HasMany(x => x.Viagens);
            HasMany(x => x.Pedidos);
            HasMany(x => x.Avaliacoes);
        }
    }
}
