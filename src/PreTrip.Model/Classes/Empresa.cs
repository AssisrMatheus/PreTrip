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
            this.Usuario = new Usuario();
        }

        public Empresa(Usuario usuario)
        {
            this.Usuario = usuario;
        }
        
        public int Id { get; set; }

        public long Cnpj { get; set; }

        public string RazaoSocial { get; set; }

        public string NomeFantasia { get; set; }
        
        public virtual Usuario Usuario { get; set; }
    }

    public class EmpresaMap : EntityTypeConfiguration<Empresa>
    {
        public EmpresaMap()
        {
            //Nome da tabela
            ToTable("Empresa");

            //Primary Key
            HasKey(x => x.Id);

            //Foreign Key
            HasRequired(x => x.Usuario);
        }
    }
}
