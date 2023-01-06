//using App.Shared.Uow;
//using Microsoft.EntityFrameworkCore;
//using Project.Domain.Entity;

//namespace Project.Infastructure.Context.Account
//{
//    public class AccountContext : ImaxDbContextBase<AccountContext>
//    {

//        public virtual DbSet<AccountEntity> Accounts { get; set; }


//        public AccountContext(DbContextOptions<AccountContext> options)
//        : base(options)
//        {
//        }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            var mutableProperties = modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string)));
//            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AccountContext).Assembly); base.OnModelCreating(modelBuilder);
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
