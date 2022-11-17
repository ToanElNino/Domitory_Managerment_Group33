using System;
using System.Data;
using System.Data.Entity;
using System.Reflection;
using System.Threading.Tasks;

namespace App.Shared.Repositories
{
    public sealed class SimpleDbContextProvider<TDbContext> : IDbContextProvider<TDbContext>
       where TDbContext : DbContext
    {
        public TDbContext DbContext { get; }

        public SimpleDbContextProvider(TDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public TDbContext GetDbContext()
        {
            return DbContext;
        }

    }
}
