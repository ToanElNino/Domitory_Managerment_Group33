using AppShared.Repositories;
using Project.Domain.Entity;

namespace Project.Domain.Infastructure
{
    public interface IBuildingRepository : IRepository<BuildingEntity, long>
    {
    }
}
