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
    public class Pedido
    {
        //Para o entity é necessário existir um construtor sem parâmetro
        public Pedido()
        {

        }

        public Pedido(Viagem viagem, Pessoa pessoa)
        {
            this.DtHrRealizacao = DateTime.Now;
            this.Quantidade = 1;

            this.ViagemId = viagem.Id;
            this.Viagem = viagem;

            this.PrecoFinal = viagem.PrecoPassagem;

            this.PessoaId = pessoa.Id;
            this.Pessoa = pessoa;

            //viagem.Pedidos.Add(this);
        }
        
        public int Id { get; set; }

        public int PessoaId { get; set; }

        public int ViagemId { get; set; }

        public int Quantidade { get; set; }

        public double PrecoFinal { get; set; }

        public DateTime DtHrRealizacao { get; set; }
        
        public virtual Viagem Viagem { get; set; }

        public virtual Pessoa Pessoa { get; set; }
    }

    public class PedidoMap : EntityTypeConfiguration<Pedido>
    {
        public PedidoMap()
        {
            //Nome da tabela
            ToTable("Pedido");

            //Primary Key
            HasKey(x => x.Id);
            
            HasRequired(x => x.Viagem).WithMany(x => x.Pedidos).HasForeignKey(x => x.ViagemId);
            
            HasRequired(x => x.Pessoa).WithMany(x => x.Pedidos).HasForeignKey(x => x.PessoaId);
        }
    }
}
