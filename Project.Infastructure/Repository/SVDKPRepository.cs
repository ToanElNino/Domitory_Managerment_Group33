using App.Shared.Repositories;
using Project.Domain.Infastructure;
using Project.Domain.Entity;

namespace Project.Infastructure.Context
{
    public class SVDKPRepository : RepositoryBase<Context, SVDKPEntity, long>, ISVDKPRepository
    {
        public SVDKPRepository(Context SVDKPContext)
            : base(SVDKPContext)
        {

        }
    }
}
