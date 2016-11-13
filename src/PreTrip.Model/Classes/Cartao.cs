using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreTrip.Model.Classes
{
    public class Cartao
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression("^[0-9]{16}$", ErrorMessage = "O cartão precisa ter exatamente 16 números")]
        public string NumeroCartao { get; set; }

        [Required]
        [RegularExpression("^[0-9]{3}$", ErrorMessage = "O código precisa ter exatamente 3 números")]
        public int Codigo { get; set; }

        [Required]
        [RegularExpression("^[0-9]{6}$", ErrorMessage = "A validade precisa ter exatamente 6 números")]
        public string DataValidade { get; set; }

        public int ContaBancariaId { get; set; }

        public ContaBancaria ContaBancaria { get; set; }
    }

    public class CartaoMap : EntityTypeConfiguration<Cartao>
    {
        public CartaoMap()
        {
            //Nome da tabela
            ToTable("cartao");

            //Primary Key
            HasKey(x => x.Id);

            HasRequired(x => x.ContaBancaria).WithMany(x => x.Cartoes).HasForeignKey(x => x.ContaBancariaId);
        }
    }
}
