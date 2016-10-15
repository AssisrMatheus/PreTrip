using PreTrip.Lib.Utils;
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
    public class Usuario
    {
        public Usuario()
        {
            this.Pessoa = new Pessoa();
        }

        public int Id { get; set; }

        [Required]
        public string Login { get; set; }

        private string senha;        
        [Required]
        public string Senha
        {
            get
            {
                if (this.IsAdmin)
                    return CreatePass.Create();

                return senha;
            }

            set
            {
                this.senha = value;
            }
        }

        public string Email { get; set; }

        public bool IsAdmin { get; set; }

        public virtual Pessoa Pessoa { get; set; }
    }

    public class UsuarioMap : EntityTypeConfiguration<Usuario>
    {
        public UsuarioMap()
        {
            //Nome da tabela
            ToTable("Usuario");

            //Primary Key
            HasKey(x => x.Id);

            Property(x => x.Login).IsRequired();
            Property(x => x.Senha).IsRequired();

            //Foreign Key
            HasRequired(x => x.Pessoa);
        }
    }
}
