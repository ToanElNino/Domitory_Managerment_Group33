using App.Shared.Uow;
using Microsoft.EntityFrameworkCore;
using Project.Domain.Entity;

namespace Project.Infastructure.Context
{
    public class Context : ImaxDbContextBase<Context>
    {

        public virtual DbSet<CanBoEntity>? CanBos { get; set; }
        public virtual DbSet<StudentEntity>? Students { get; set; }
        public virtual DbSet<BuildingEntity>? Buildings { get; set; }
        public virtual DbSet<RoomEntity>? Rooms { get; set; }
        public virtual DbSet<SVDKPEntity>? SVDKPs { get; set; }
        public virtual DbSet<SVTPEntity>? SVTPs { get; set; }
        public virtual DbSet<AccountEntity>? Accounts { get; set; }
        public Context(DbContextOptions<Context> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var mutableProperties = modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string)));
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly); base.OnModelCreating(modelBuilder);
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
