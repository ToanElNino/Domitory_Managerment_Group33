using App.Shared.Repositories;
using Project.Domain.Infastructure;
using Project.Domain.Entity;
using Project.Infastructure.Context.Building;

namespace Project.Infastructure.Repositories
{
    public class BuildingRepository : RepositoryBase<BuildingContext, BuildingEntity, long>, IBuildingRepository
    {
        public BuildingRepository(BuildingContext BuildingContext)
            : base(BuildingContext)
        {

        }
    }
}
