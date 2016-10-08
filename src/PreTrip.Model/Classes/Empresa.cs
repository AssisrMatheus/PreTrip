using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreTrip.Model.Classes
{
    [Table("Empresa")]
    public class Empresa
    {
        [Key]
        public int Id { get; set; }

        public long Cnpj { get; set; }

        public string RazaoSocial { get; set; }

        public string NomeFantasia { get; set; }

        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }

        public virtual Usuario Usuario { get; set; }

        public Empresa()
        {

        }

        public Empresa(Usuario usuario)
        {
            this.UsuarioId = usuario.Id;
            this.Usuario = usuario;
        }

        //[ForeignKey("IdEndereco")]
        //public Endereco Endereco { get; set; }
    }
}
