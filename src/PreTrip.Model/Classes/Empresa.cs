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
    public class Empresa
    {
        public Empresa()
        {

        }
        
        public int Id { get; set; }

        [Required]
        public int Cnpj { get; set; }

        [Required]
        public string RazaoSocial { get; set; }

        public string NomeFantasia { get; set; }
    }

    public class EmpresaMap : EntityTypeConfiguration<Empresa>
    {
        public EmpresaMap()
        {
            //Nome da tabela
            ToTable("Empresa");

            //Primary Key
            HasKey(x => x.Id);
        }
    }
}
