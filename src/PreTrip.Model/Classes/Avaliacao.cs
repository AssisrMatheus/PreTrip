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
        public int Id { get; set; }

        public int Nota { get; set; }

        public string Comentario { get; set; }

        public virtual Usuario Usuario { get; set; }
        
        public virtual Viagem Viagem { get; set; }
    }

    public class AvaliacaoMap : EntityTypeConfiguration<Avaliacao>
    {
        public AvaliacaoMap()
        {
            //Nome da tabela
            ToTable("Avaliacao");

            //Primary Key
            HasKey(x => x.Id);

            HasRequired(x => x.Usuario);
            HasRequired(x => x.Viagem);
        }
    }
}
