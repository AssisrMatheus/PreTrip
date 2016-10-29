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
    public class Endereco
    {
        public int Id { get; set; }

        public int Numero { get; set; }

        public int Complemento { get; set; }

        public string Rua { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }
    }

    public class EnderecoMap : EntityTypeConfiguration<Endereco>
    {
        public EnderecoMap()
        {
            //Nome da tabela
            ToTable("Endereco");

            //Primary Key
            HasKey(x => x.Id);
        }
    }
}
