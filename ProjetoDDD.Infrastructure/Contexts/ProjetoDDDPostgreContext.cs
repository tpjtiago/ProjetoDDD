using ProjetoDDD.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace ProjetoDDD.Infrastructure.Contexts
{
    public class ProjetoDDDPostgreContext : DbContext
    {
        public ProjetoDDDPostgreContext(DbContextOptions<ProjetoDDDPostgreContext> opt)
            : base(opt) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DemoMap());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("User ID=postgres;Password=local123;Server=localhost;Port=15432;Database=ProjetoDDD;Integrated Security=true;Pooling=true;");
        }
    }
}