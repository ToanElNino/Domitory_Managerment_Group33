
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace App.Shared.Uow
{
    public class UnitOfWorkBase<TDbContext> : IMaxUnitOfWork where TDbContext : IImaxDbConext
    {
        private TDbContext dataContext;
        IDbContextTransaction transaction;

        public UnitOfWorkBase(TDbContext modelContext)
        {
            this.dataContext = modelContext;
        }


        public virtual void BeginTransaction()
        {
            this.transaction = dataContext.GetDatabase().BeginTransaction();
        }

        public virtual void SaveChange(long? userId = null, int? tenantId = null)
        {
            dataContext.SaveChanges(userId);
        }

        public virtual Task SaveChangesAsync(long? userId = null, int? tenantId = null)
        {
            return dataContext.SaveChangesAsync(userId);
        }


        public void RollBack()
        {
            this.transaction.Rollback();
        }

        public virtual void Commit(long? userId = null, int? tenantId = null)
        {
            dataContext.SaveChanges(userId);
            //this.transaction.Commit();
        }
    }
}
