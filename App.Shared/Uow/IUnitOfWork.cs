
using System.Threading.Tasks;

namespace App.Shared.Uow
{
    public interface IMaxUnitOfWork
    {
        void SaveChange(long? userId = null, int? tenantId = null);
        Task SaveChangesAsync(long? userId = null, int? tenantId = null);
        void BeginTransaction();
        void Commit(long? userId = null, int? tenantId = null);
        void RollBack();
    }
}
