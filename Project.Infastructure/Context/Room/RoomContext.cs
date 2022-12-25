using App.Shared.Uow;
using Microsoft.EntityFrameworkCore;
using Project.Domain.Entity;

namespace Project.Infastructure.Context.Room
{
    public class RoomContext : ImaxDbContextBase<RoomContext>
    {

        public virtual DbSet<RoomEntity> Rooms { get; set; }


        public RoomContext(DbContextOptions<RoomContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var mutableProperties = modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string)));
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RoomContext).Assembly); base.OnModelCreating(modelBuilder);
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
