using System.Data.Entity;

namespace App.Shared.Repositories
{
    public interface IRepositoryWithDbContext
    {
        DbContext GetDbContext();
    }
}