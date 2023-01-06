using AppShared.Repositories;
using Project.Domain.Entity;

namespace Project.Domain.Infastructure
{
    public interface ICanBoRepository : IRepository<CanBoEntity, long>
    {
    }
}
