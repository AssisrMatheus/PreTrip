namespace PreTrip.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class PreTripDB : DbContext
    {
        public PreTripDB()
            : base("name=PreTripDB")
        {
        }

        public virtual DbSet<contas> contas { get; set; }
        public virtual DbSet<empresas> empresas { get; set; }
        public virtual DbSet<enderecos> enderecos { get; set; }
        public virtual DbSet<eventos> eventos { get; set; }
        public virtual DbSet<pedidos> pedidos { get; set; }
        public virtual DbSet<pessoas> pessoas { get; set; }
        public virtual DbSet<usuarios> usuarios { get; set; }
        public virtual DbSet<veiculos> veiculos { get; set; }
        public virtual DbSet<viagens> viagens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<empresas>()
                .Property(e => e.CNPJ)
                .IsUnicode(false);

            modelBuilder.Entity<empresas>()
                .Property(e => e.RAZAO_SOCIAL)
                .IsUnicode(false);

            modelBuilder.Entity<empresas>()
                .Property(e => e.NM_FANTASIA)
                .IsUnicode(false);

            modelBuilder.Entity<enderecos>()
                .Property(e => e.RUA)
                .IsUnicode(false);

            modelBuilder.Entity<enderecos>()
                .Property(e => e.BAIRRO)
                .IsUnicode(false);

            modelBuilder.Entity<enderecos>()
                .Property(e => e.CIDADE)
                .IsUnicode(false);

            modelBuilder.Entity<enderecos>()
                .Property(e => e.ESTADO)
                .IsUnicode(false);

            modelBuilder.Entity<eventos>()
                .Property(e => e.NM_EVENTO)
                .IsUnicode(false);

            modelBuilder.Entity<eventos>()
                .Property(e => e.TAGS)
                .IsUnicode(false);

            modelBuilder.Entity<eventos>()
                .HasMany(e => e.viagens)
                .WithMany(e => e.eventos)
                .Map(m => m.ToTable("viagem_evento", "pretrip").MapLeftKey("ID_EVENTO").MapRightKey("ID_VIAGEM"));

            modelBuilder.Entity<pedidos>()
                .HasMany(e => e.usuarios)
                .WithMany(e => e.pedidos)
                .Map(m => m.ToTable("usuario_pedido", "pretrip").MapLeftKey("ID_PEDIDO").MapRightKey("ID_USUARIO"));

            modelBuilder.Entity<pessoas>()
                .Property(e => e.NM_PESSOA)
                .IsUnicode(false);

            modelBuilder.Entity<pessoas>()
                .Property(e => e.DATA_NASC)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios>()
                .Property(e => e.LOGIN)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios>()
                .Property(e => e.SENHA)
                .IsUnicode(false);

            modelBuilder.Entity<usuarios>()
                .HasMany(e => e.viagens)
                .WithMany(e => e.usuarios)
                .Map(m => m.ToTable("usuario_viagem", "pretrip").MapLeftKey("ID_USUARIO").MapRightKey("ID_VIAGEM"));

            modelBuilder.Entity<veiculos>()
                .Property(e => e.MODELO)
                .IsUnicode(false);

            modelBuilder.Entity<veiculos>()
                .Property(e => e.PLACA)
                .IsUnicode(false);

            modelBuilder.Entity<veiculos>()
                .Property(e => e.TIPO)
                .IsUnicode(false);

            modelBuilder.Entity<viagens>()
                .Property(e => e.DS_VIAGEM)
                .IsUnicode(false);
        }
    }
}
