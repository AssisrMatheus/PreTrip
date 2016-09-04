using PreTrip.Model.Classes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreTrip.Model.Context
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class PreTripDB : DbContext
    {
        public PreTripDB() :base() {}

        public DbSet<Viagem> Viagens { get; set; }

        public DbSet<Veiculo> Veiculos { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Pessoa> Pessoas { get; set; }

        public DbSet<Pedido> Pedidos { get; set; }

        public DbSet<Evento> Eventos { get; set; }

        public DbSet<Endereco> Enderecos { get; set; }

        public DbSet<Empresa> Empresas { get; set; }

        public DbSet<ContaBancaria> ContasBancarias { get; set; }

        public DbSet<Busca> Buscas { get; set; }

        public DbSet<Avaliacao> Avaliacoes { get; set; }

        public DbSet<Interesse> UsuarioInteresses { get; set; }
    }
}
