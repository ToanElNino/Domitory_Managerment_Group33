﻿//using App.Shared.Uow;
//using Microsoft.EntityFrameworkCore;
//using Project.Domain.Entity;

//namespace Project.Infastructure.Context.Student
//{
//    public class StudentContext : ImaxDbContextBase<StudentContext>
//    {

//        public virtual DbSet<StudentEntity> Students { get; set; }


//        public StudentContext(DbContextOptions<StudentContext> options)
//        : base(options)
//        {
//        }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            var mutableProperties = modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string)));
//            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StudentContext).Assembly); base.OnModelCreating(modelBuilder);
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
