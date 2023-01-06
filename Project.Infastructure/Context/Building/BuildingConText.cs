//using App.Shared.Uow;
//using Microsoft.EntityFrameworkCore;
//using Project.Domain.Entity;

//namespace Project.Infastructure.Context.Building
//{
//    public class BuildingContext : ImaxDbContextBase<BuildingContext>
//    {

//        public virtual DbSet<BuildingEntity> Buildings { get; set; }


//        public BuildingContext(DbContextOptions<BuildingContext> options)
//        : base(options)
//        {
//        }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            var mutableProperties = modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string)));
//            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BuildingContext).Assembly); base.OnModelCreating(modelBuilder);
//        }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//                optionsBuilder.UseSqlServer("DefaultConnection");

//            }
//        }

//    }
//}
