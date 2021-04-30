using CursoEFCore.Data.Configurations;
using CursoEFCore.Domain;
using Microsoft.EntityFrameworkCore;

namespace CursoEFCore.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Pedido> Pedidos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data source=(localdb)\\mssqllocaldb;Initial Catalog=CursoEFCore; Integrated Security=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.ApplyConfiguration(new ClienteConfiguration());
            // modelBuilder.ApplyConfiguration(new PedidoConfiguration());
            // modelBuilder.ApplyConfiguration(new PedidoItemConfiguration());
            // modelBuilder.ApplyConfiguration(new ProdutoConfiguration());

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
        }
    }
}
