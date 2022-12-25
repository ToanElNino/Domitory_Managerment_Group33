using Abp.Domain.Entities;
using Abp.Extensions;
using AppShared.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Shared.Repositories
{
    public class RepositoryBase<TDbContext, TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
        where TDbContext : DbContext
    {

        public TDbContext Context;
        public RepositoryBase(TDbContext context)
        {
            Context = context;
        }

        public  CancellationToken CancelToken => CancellationToken.None;
       

        /// <summary>
        /// Gets DbSet for given entity.
        /// </summary>
        public virtual DbSet<TEntity> Table => Context.Set<TEntity>();


        public  IQueryable<TEntity> GetAll()
        {
            return Table;
        }

        public Task<IQueryable<TEntity>> GetAllAsync()
        {
            return Task.FromResult(Table.AsQueryable());
        }


        public async Task<List<TEntity>> GetAllListAsync()
        {
            var query = await GetAllAsync();
            return await query.ToListAsync(CancelToken);
        }

        public async Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var query = await GetAllAsync();
            return await query.Where(predicate).ToListAsync(CancelToken);
        }

        public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var query = await GetAllAsync();
            return await query.SingleAsync(predicate, CancelToken);
        }

        public virtual TEntity FirstOrDefault(TPrimaryKey id)
        {
            return GetAll().FirstOrDefault(CreateEqualityExpressionForId(id));
        }

        public async Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id)
        {
            var query = await GetAllAsync();
            return await query.FirstOrDefaultAsync(CreateEqualityExpressionForId(id), CancelToken);
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var query = await GetAllAsync();
            return await query.FirstOrDefaultAsync(predicate, CancelToken);
        }

        public TEntity Insert(TEntity entity)
        {
            Table.Add(entity);
            Context.Entry(entity).State = EntityState.Added;
            return entity;
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
             await Table.AddAsync(entity);
            Context.Entry(entity).State = EntityState.Added;
            return entity;
        }

        public TPrimaryKey InsertAndGetId(TEntity entity)
        {
            entity = Insert(entity);

            if (entity.IsTransient())
            {
                Context.SaveChanges();
            }

            return entity.Id;
        }

        public async Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity)
        {
            entity = await InsertAsync(entity);

            if (entity.IsTransient())
            {
                await Context.SaveChangesAsync(CancelToken);
            }

            return entity.Id;
        }


        public TEntity Update(TEntity entity)
        {
            AttachIfNot(entity);
            Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();
            return entity;
        }

        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            Update(entity);
            return Task.FromResult(entity);
        }

        public void Delete(TEntity entity)
        {
            AttachIfNot(entity);
            if (!(entity is ISoftDelete))
            {
                Table.Remove(entity);
            }
            else
            {
                entity.As<ISoftDelete>().IsDeleted = true;
            }
            Context.SaveChanges();
        }
        public void Delete(TPrimaryKey id)
        {
            var entity = Table.Local.FirstOrDefault(ent => EqualityComparer<TPrimaryKey>.Default.Equals(ent.Id, id));
            if (entity == null)
            {
                entity = FirstOrDefault(id);
                if (entity == null)
                {
                    return;
                }
            }

            Delete(entity);
        }

        public async Task<int> CountAsync()
        {
            var query = await GetAllAsync();
            return await query.CountAsync(CancelToken);
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var query = await GetAllAsync();
            return await query.Where(predicate).CountAsync(CancelToken);
        }

        public async Task<long> LongCountAsync()
        {
            var query = await GetAllAsync();
            return await query.LongCountAsync(CancelToken);
        }

        public async Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var query = await GetAllAsync();
            return await query.Where(predicate).LongCountAsync(CancelToken);
        }

        protected virtual void AttachIfNot(TEntity entity)
        {
            if (!Table.Local.Contains(entity))
            {
                Table.Attach(entity);
            }
        }

        protected virtual Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TPrimaryKey id)
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));

            var leftExpression = Expression.PropertyOrField(lambdaParam, "Id");

            var idValue = Convert.ChangeType(id, typeof(TPrimaryKey));

            Expression<Func<object>> closure = () => idValue;
            var rightExpression = Expression.Convert(closure.Body, leftExpression.Type);

            var lambdaBody = Expression.Equal(leftExpression, rightExpression);

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }


        public virtual List<TEntity> GetAllList()
        {
            return GetAll().ToList();
        }

        public virtual List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate).ToList();
        }


        public virtual T Query<T>(Func<IQueryable<TEntity>, T> queryMethod)
        {
            return queryMethod(GetAll());
        }

        public virtual TEntity Get(TPrimaryKey id)
        {
            var entity = FirstOrDefault(id);
            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(TEntity), id);
            }

            return entity;
        }


        public virtual async Task<TEntity> GetAsync(TPrimaryKey id)
        {
            var entity = await FirstOrDefaultAsync(id);
            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(TEntity), id);
            }

            return entity;
        }

        public virtual TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Single(predicate);
        }


        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }


        public virtual TEntity Load(TPrimaryKey id)
        {
            return Get(id);
        }

        public virtual TEntity Update(TPrimaryKey id, Action<TEntity> updateAction)
        {
            var entity = Get(id);
            updateAction(entity);
            return entity;
        }



        public virtual async Task<TEntity> UpdateAsync(TPrimaryKey id, Func<TEntity, Task> updateAction)
        {
            var entity = await GetAsync(id);
            await updateAction(entity);
            return entity;
        }

        public virtual int Count()
        {
            return GetAll().Count();
        }

        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Count(predicate);
        }

        public virtual long LongCount()
        {
            return GetAll().LongCount();
        }

        public virtual long LongCount(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().LongCount(predicate);
        }

        public virtual Task DeleteAsync(TEntity entity)
        {
            Delete(entity);
            return Task.CompletedTask;
        }
        public virtual Task DeleteAsync(TPrimaryKey id)
        {
            Delete(id);
            return Task.CompletedTask;
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            foreach (var entity in GetAllList(predicate))
            {
                Delete(entity);
            }
        }

        public virtual async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = await GetAllListAsync(predicate);

            foreach (var entity in entities)
            {
                await DeleteAsync(entity);
            }
        }

    }
}
