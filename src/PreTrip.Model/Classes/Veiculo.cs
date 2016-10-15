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
    public class Veiculo
    {
        public int Id { get; set; }

        public string Modelo { get; set; }

        public string Placa { get; set; }

        public string Tipo { get; set; }

        public int QuantidadeLugares { get; set; }
    }

    public class VeiculoMap : EntityTypeConfiguration<Veiculo>
    {
        public VeiculoMap()
        {
            //Nome da tabela
            ToTable("veiculo");

            //Primary Key
            HasKey(x => x.Id);            
        }
    }
}
