using Abp.Auditing;
using Abp.Authorization;
using Abp.Configuration;
using Abp.EntityFrameworkCore;
using Abp.EntityHistory;
using Abp.Localization;
using Abp.Notifications;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Abp.DynamicEntityProperties;
using Abp.Webhooks;

namespace Abp.Zero.EntityFrameworkCore
{
    public abstract class AbpZeroCommonDbContext<T> : AbpDbContext
     
        where T : AbpZeroCommonDbContext<T>
    {
       
        /// <summary>
        /// Tenant notifications.
        /// </summary>
        public virtual DbSet<TenantNotificationInfo> TenantNotifications { get; set; }

        /// <summary>
        /// User notifications.
        /// </summary>
        public virtual DbSet<UserNotificationInfo> UserNotifications { get; set; }

        /// <summary>
        /// Notification subscriptions.
        /// </summary>
        public virtual DbSet<NotificationSubscriptionInfo> NotificationSubscriptions { get; set; }

        /// <summary>
        /// Entity changes.
        /// </summary>
        public virtual DbSet<EntityChange> EntityChanges { get; set; }

        /// <summary>
        /// Entity change sets.
        /// </summary>
        public virtual DbSet<EntityChangeSet> EntityChangeSets { get; set; }

        /// <summary>
        /// Entity property changes.
        /// </summary>
        public virtual DbSet<EntityPropertyChange> EntityPropertyChanges { get; set; }

        /// <summary>
        /// Webhook information
        /// </summary>
        public virtual DbSet<WebhookEvent> WebhookEvents { get; set; }

        /// <summary>
        /// Web subscriptions  
        /// </summary>
        public virtual DbSet<WebhookSubscriptionInfo> WebhookSubscriptions { get; set; }

        /// <summary>
        /// Webhook work items
        /// </summary>
        public virtual DbSet<WebhookSendAttempt> WebhookSendAttempts { get; set; }

        /// <summary>
        /// DynamicProperties
        /// </summary>
        public virtual DbSet<DynamicProperty> DynamicProperties { get; set; }

        /// <summary>
        /// DynamicProperty selectable values
        /// </summary>
        public virtual DbSet<DynamicPropertyValue> DynamicPropertyValues { get; set; }

        /// <summary>
        /// Entities dynamic properties. Which property that entity has
        /// </summary>
        public virtual DbSet<DynamicEntityProperty> DynamicEntityProperties { get; set; }

        /// <summary>
        /// Entities dynamic properties values
        /// </summary>
        public virtual DbSet<DynamicEntityPropertyValue> DynamicEntityPropertyValues { get; set; }
        public IEntityHistoryHelper EntityHistoryHelper { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        protected AbpZeroCommonDbContext(DbContextOptions<T> options)
            : base(options)
        {

        }

        public override int SaveChanges()
        {
            var changeSet = EntityHistoryHelper?.CreateEntityChangeSet(ChangeTracker.Entries().ToList());

            var result = base.SaveChanges();

            EntityHistoryHelper?.Save(changeSet);

            return result;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var changeSet = EntityHistoryHelper?.CreateEntityChangeSet(ChangeTracker.Entries().ToList());

            var result = await base.SaveChangesAsync(cancellationToken);

            if (EntityHistoryHelper != null)
            {
                await EntityHistoryHelper.SaveAsync(changeSet);
            }

            return result;
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