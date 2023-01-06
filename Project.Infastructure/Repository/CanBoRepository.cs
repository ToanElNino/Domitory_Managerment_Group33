using App.Shared.Repositories;
using Project.Domain.Infastructure;
using Project.Domain.Entity;
using Project.Infastructure.Context;

namespace Project.Infastructure.Context
{
    public class CanBoRepository : RepositoryBase<Context, CanBoEntity, long>, ICanBoRepository
    {
        public CanBoRepository(Context context)
            : base(context)
        {

        }
    }
}
