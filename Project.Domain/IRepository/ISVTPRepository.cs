using AppShared.Repositories;
using Project.Domain.Entity;

namespace Project.Domain.Infastructure
{
    public interface ISVTPRepository : IRepository<SVTPEntity, long>
    {
    }
}
