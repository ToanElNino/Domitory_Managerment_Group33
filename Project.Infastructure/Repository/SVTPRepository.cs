using App.Shared.Repositories;
using Project.Domain.Infastructure;
using Project.Domain.Entity;
using Project.Infastructure.Context.SVTP;

namespace Project.Infastructure.Repositories
{
    public class SVTPRepository : RepositoryBase<SVTPContext, SVTPEntity, long>, ISVTPRepository
    {
        public SVTPRepository(SVTPContext SVTPContext)
            : base(SVTPContext)
        {

        }
    }
}
