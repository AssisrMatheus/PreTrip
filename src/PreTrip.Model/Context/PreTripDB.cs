using PreTrip.Model.Classes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreTrip.Model.Context
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class PreTripDB : DbContext
    {
        public PreTripDB() :base()
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;          
        }

        public DbSet<Viagem> Viagens { get; set; }

        public DbSet<Veiculo> Veiculos { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Pessoa> Pessoas { get; set; }

        public DbSet<Pedido> Pedidos { get; set; }

        //public DbSet<Evento> Eventos { get; set; }

        public DbSet<Endereco> Enderecos { get; set; }

        public DbSet<Empresa> Empresas { get; set; }

        public DbSet<ContaBancaria> ContasBancarias { get; set; }

        public DbSet<Busca> Buscas { get; set; }

        public DbSet<Avaliacao> Avaliacoes { get; set; }

        public DbSet<Interesse> Interesses { get; set; }
        public object Entity { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new ViagemMap());
            modelBuilder.Configurations.Add(new UsuarioMap());
            modelBuilder.Configurations.Add(new PessoaMap());
            modelBuilder.Configurations.Add(new PedidoMap());
            modelBuilder.Configurations.Add(new InteresseMap());
            //modelBuilder.Configurations.Add(new EventoMap());
            modelBuilder.Configurations.Add(new EnderecoMap());
            modelBuilder.Configurations.Add(new EmpresaMap());
            modelBuilder.Configurations.Add(new ContaBancariaMap());
            modelBuilder.Configurations.Add(new BuscaMap());
            modelBuilder.Configurations.Add(new AvaliacaoMap());
        }
    }
}
