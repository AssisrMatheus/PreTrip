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
    public class Busca
    {             
        public Busca(Usuario usuario)
        {
            DtHrBusca = DateTime.Now;
            this.Usuario = usuario;
        }
        
        public int Id { get; set; }

        public string Titulo { get; set; }

        public double Preco { get; set; }

        public int QuantidadeLugares { get; set; }

        public int LugaresDisponiveis { get; set; }

        public string Origem { get; set; }

        public string Destino { get; set; }

        public DateTime DtHrBusca { get; set; }
        
        public virtual Usuario Usuario { get; set; }
    }

    public class BuscaMap : EntityTypeConfiguration<Busca>
    {
        public BuscaMap()
        {
            //Nome da tabela
            ToTable("Busca");

            //Primary Key
            HasKey(x => x.Id);

            HasRequired(x => x.Usuario);
        }
    }
}
