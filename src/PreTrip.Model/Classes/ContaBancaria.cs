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
    public class ContaBancaria
    {
        public int Id { get; set; }

        public double Saldo { get; set; }

        public virtual Pessoa Pessoa { get; set; }

        public ICollection<Cartao> Cartoes { get; set; }

        public ContaBancaria()
        {
            this.Cartoes = new List<Cartao>();
        }
    }

    public class ContaBancariaMap : EntityTypeConfiguration<ContaBancaria>
    {
        public ContaBancariaMap()
        {
            //Nome da tabela
            ToTable("contabancaria");

            //Primary Key
            HasKey(x => x.Id);

            HasRequired(x => x.Pessoa).WithRequiredDependent(x => x.ContaBancaria);

            HasMany(x => x.Cartoes);
        }
    }
}
