//using App.Shared.Uow;
//using Microsoft.EntityFrameworkCore;
//using Project.Domain.Entity;

//namespace Project.Infastructure.Context.SVTP
//{
//    public class SVTPContext : ImaxDbContextBase<SVTPContext>
//    {

//        public virtual DbSet<SVTPEntity> SVTPs { get; set; }


//        public SVTPContext(DbContextOptions<SVTPContext> options)
//        : base(options)
//        {
//        }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            var mutableProperties = modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string)));
//            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SVTPContext).Assembly); base.OnModelCreating(modelBuilder);
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
