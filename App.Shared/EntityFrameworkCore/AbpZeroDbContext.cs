
using Abp.Application.Features;
using Abp.Auditing;
using Abp.BackgroundJobs;
using Abp.MultiTenancy;
using Abp.Notifications;
using Microsoft.EntityFrameworkCore;

namespace Abp.Zero.EntityFrameworkCore
{
    /// <summary>
    /// Base DbContext for ABP zero.
    /// Derive your DbContext from this class to have base entities.
    /// </summary>
    public abstract class AbpZeroDbContext<T> : AbpZeroCommonDbContext<T>
        where T : AbpZeroDbContext<T>
    {
       
        protected AbpZeroDbContext(DbContextOptions<T> options)
            : base(options)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
        }
    }
}
