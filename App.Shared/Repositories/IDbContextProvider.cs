using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace App.Shared.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public interface IDbContextProvider<TDbContext>
        where TDbContext : DbContext
    {
        TDbContext GetDbContext();
    }
}
