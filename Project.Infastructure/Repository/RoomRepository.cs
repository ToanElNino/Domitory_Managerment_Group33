using App.Shared.Repositories;
using Project.Domain.Infastructure;
using Project.Domain.Entity;
using Project.Infastructure.Context.Room;

namespace Project.Infastructure.Repositories
{
    public class RoomRepository : RepositoryBase<RoomContext, RoomEntity, long>, IRoomRepository
    {
        public RoomRepository(RoomContext RoomContext)
            : base(RoomContext)
        {

        }
    }
}
