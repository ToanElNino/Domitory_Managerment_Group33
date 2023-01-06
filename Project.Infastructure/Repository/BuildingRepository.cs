using App.Shared.Repositories;
using Project.Domain.Infastructure;
using Project.Domain.Entity;
namespace Project.Infastructure.Context
{
    public class BuildingRepository : RepositoryBase<Context, BuildingEntity, long>, IBuildingRepository
    {
        public BuildingRepository(Context BuildingContext)
            : base(BuildingContext)
        {

        }
    }
}
