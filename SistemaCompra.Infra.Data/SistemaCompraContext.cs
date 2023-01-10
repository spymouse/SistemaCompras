using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SistemaCompra.Domain.Core;
using SistemaCompra.Infra.Data.Produto;
using SistemaCompra.Infra.Data.SolicitacaoCompra;
using ProdutoAgg = SistemaCompra.Domain.ProdutoAggregate;
using SolicitacaoAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;

namespace SistemaCompra.Infra.Data
{
    public class SistemaCompraContext : DbContext
    {
        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public SistemaCompraContext(DbContextOptions options) : base(options) { }
        public DbSet<ProdutoAgg.Produto> Produtos { get; set; }
        public DbSet<SolicitacaoAgg.SolicitacaoCompra> SolicitacaoCompra { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<ProdutoAgg.Produto>()
            //    .HasData(
            //        new ProdutoAgg.Produto("Produto01", "Descricao01", "Madeira", 100)
            //    );

            modelBuilder.Entity<SolicitacaoAgg.SolicitacaoCompra>().OwnsOne(x => x.NomeFornecedor );
            modelBuilder.Entity<SolicitacaoAgg.SolicitacaoCompra>().OwnsOne(x => x.UsuarioSolicitante);

            modelBuilder.Entity<SolicitacaoAgg.SolicitacaoCompra>().Property(p => p.TotalGeral).HasColumnType("decimal(18,4)");
            modelBuilder.Ignore<Event>();

            modelBuilder.ApplyConfiguration(new ProdutoConfiguration());
            modelBuilder.ApplyConfiguration(new SolicitacaoCompraConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(loggerFactory)  
                .EnableSensitiveDataLogging()
                .UseSqlServer(@"Server=DESKTOP-4MMNMOS\\SQLEXPRESS; Database=SistemaCompraDb; Trusted_Connection=True;");
        }
    }
}
