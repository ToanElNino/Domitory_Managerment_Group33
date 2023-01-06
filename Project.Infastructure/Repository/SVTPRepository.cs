using App.Shared.Repositories;
using Project.Domain.Infastructure;
using Project.Domain.Entity;

namespace Project.Infastructure.Context
{
    public class SVTPRepository : RepositoryBase<Context, SVTPEntity, long>, ISVTPRepository
    {
        public SVTPRepository(Context SVTPContext)
            : base(SVTPContext)
        {

        }
    }
}
