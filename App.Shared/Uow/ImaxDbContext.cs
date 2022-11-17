using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Extensions;
using Abp.Timing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace App.Shared.Uow
{
    public interface IImaxDbConext
    {
        Task<int> SaveChangesAsync(long? userId = null, CancellationToken cancellationToken = new CancellationToken());
        int SaveChanges(long? userId = null);
        //Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
        //int SaveChanges();
        DatabaseFacade GetDatabase(); 

    }

    public abstract class ImaxDbContextBase<TSelf> : DbContext, IImaxDbConext
        where TSelf : ImaxDbContextBase<TSelf>
    {
        protected ImaxDbContextBase(DbContextOptions<TSelf> options)
           : base(options)
        {
        }

        public DatabaseFacade GetDatabase()
        {
            return Database;
        }

        public Task<int> SaveChangesAsync(long? userId = null, CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                ApplyConcepts(entry, userId);
            }
            return SaveChangesAsync(cancellationToken);
        }

        public int SaveChanges(long? userId = null)
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                ApplyConcepts(entry, userId);
            }
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            //foreach (var entry in ChangeTracker.Entries())
            //{
            //    ApplyConcepts(entry, null);
            //}
            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            //foreach (var entry in ChangeTracker.Entries())
            //{
            //    ApplyConcepts(entry, null);
            //}
            return base.SaveChanges();
        }

        protected virtual void ApplyConcepts(EntityEntry entry, long? userId)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    ApplyConceptsForAddedEntity(entry, userId);
                    break;
                case EntityState.Modified:
                    ApplyConceptsForModifiedEntity(entry, userId);
                    break;
                //case EntityState.Deleted:
                //    ApplyConceptsForModifiedEntity(entry, userId);
                //    break;
            }

        }

        protected virtual void ApplyConceptsForAddedEntity(EntityEntry entry, long? userId)
        {
            SetCreationAuditProperties(entry.Entity, userId);
        }
        protected virtual void ApplyConceptsForModifiedEntity(EntityEntry entry, long? userId)
        {
            SetModificationAuditProperties(entry.Entity, userId);

        }


        protected virtual void SetCreationAuditProperties(object entityAsObj, long? userId)
        {
            var entityWithCreationTime = entityAsObj as IHasCreationTime;
            if (entityWithCreationTime == null)
            {
                //Object does not implement IHasCreationTime
                return;
            }

            //if (entityWithCreationTime.CreationTime == default)
            //{
            //    entityWithCreationTime.CreationTime = System.DateTime.Now;
            //}
            entityWithCreationTime.CreationTime = DateTime.Now;
            if (!(entityAsObj is ICreationAudited))
            {
                //Object does not implement ICreationAudited
                return;
            }

            if (!userId.HasValue)
            {
                //Unknown user
                return;
            }

            var entity = entityAsObj as ICreationAudited;
            if (entity.CreatorUserId != null)
            {
                //CreatorUserId is already set
                return;
            }

            //Finally, set CreatorUserId!
            entity.CreatorUserId = userId;
        }

        protected virtual void SetModificationAuditProperties(object entityAsObj, long? userId)
        {
            if (entityAsObj is IHasModificationTime)
            {
                entityAsObj.As<IHasModificationTime>().LastModificationTime = System.DateTime.Now;
            }

            if (!(entityAsObj is IModificationAudited))
            {
                //Entity does not implement IModificationAudited
                return;
            }

            var entity = entityAsObj.As<IModificationAudited>();

            if (userId == null)
            {
                //Unknown user
                entity.LastModifierUserId = null;
                return;
            }

            //Finally, set LastModifierUserId!
            entity.LastModifierUserId = userId;
        }

    }
}
