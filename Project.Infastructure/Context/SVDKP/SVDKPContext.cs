using App.Shared.Uow;
using Microsoft.EntityFrameworkCore;
using Project.Domain.Entity;

namespace Project.Infastructure.Context.SVDKP
{
    public class SVDKPContext : ImaxDbContextBase<SVDKPContext>
    {

        public virtual DbSet<SVDKPEntity> SVDKPs { get; set; }


        public SVDKPContext(DbContextOptions<SVDKPContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var mutableProperties = modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string)));
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SVDKPContext).Assembly); base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("DefaultConnection");

            }
        }

    }
}
