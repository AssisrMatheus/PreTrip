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
    public class Evento
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Tags { get; set; }
        
        public virtual Viagem Viagem { get; set; }
    }

    public class EventoMap : EntityTypeConfiguration<Evento>
    {
        public EventoMap()
        {
            //Nome da tabela
            ToTable("Evento");

            //Primary Key
            HasKey(x => x.Id);

            //Foreign Key
            HasRequired(x => x.Viagem);
        }
    }
}
