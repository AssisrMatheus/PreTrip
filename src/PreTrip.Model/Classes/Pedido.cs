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
        public Pedido()
        {
            this.DtHrRealizacao = DateTime.Now;
            this.Quantidade = 1;
        }

        public Pedido(Viagem viagem)
        {
            this.DtHrRealizacao = DateTime.Now;
            this.Quantidade = 1;

            this.Viagem = viagem;
            this.PrecoFinal = viagem.PrecoPassagem;
        }
        
        public int Id { get; set; }

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

            //Foreign Key
            HasRequired(x => x.Viagem);
            HasRequired(x => x.Pessoa);            
        }
    }
}
