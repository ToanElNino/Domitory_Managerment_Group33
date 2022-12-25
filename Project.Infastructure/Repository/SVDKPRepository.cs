using App.Shared.Repositories;
using Project.Domain.Infastructure;
using Project.Domain.Entity;
using Project.Infastructure.Context.SVDKP;

namespace Project.Infastructure.Repositories
{
    public class SVDKPRepository : RepositoryBase<SVDKPContext, SVDKPEntity, long>, ISVDKPRepository
    {
        public SVDKPRepository(SVDKPContext SVDKPContext)
            : base(SVDKPContext)
        {

        }
    }
}
