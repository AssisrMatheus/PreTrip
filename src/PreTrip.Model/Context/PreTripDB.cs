using PreTrip.Model.Classes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreTrip.Model.Context
{
    public class PreTripDB : DbContext
    {
        public PreTripDB()
            :base()
        {

        }

        public DbSet<Viagem> Viagem { get; set; }

        public DbSet<Veiculo> Veiculo { get; set; }

        public DbSet<Usuario> Usuario { get; set; }

        public DbSet<Pessoa> Pessoa { get; set; }

        public DbSet<Pedido> Pedido { get; set; }

        public DbSet<Evento> Evento { get; set; }

        public DbSet<Endereco> Endereco { get; set; }

        public DbSet<Empresa> Empresa { get; set; }

        public DbSet<Conta> Conta { get; set; }
    }
}
