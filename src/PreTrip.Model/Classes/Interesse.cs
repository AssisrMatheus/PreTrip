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
    public class Interesse
    {
        public Interesse()
        {
            Pessoa = new Pessoa();
        }

        [Key]
        public int Id { get; set; }
        
        public string Cidade { get; set; }

        public virtual Pessoa Pessoa { get; set; }
    }

    public class InteresseMap : EntityTypeConfiguration<Interesse>
    {
        public InteresseMap()
        {
            //Nome da tabela
            ToTable("Interesse");

            //Primary Key
            HasKey(x => x.Id);

            //Foreign Key
            HasRequired(x => x.Pessoa);
        }
    }
}
