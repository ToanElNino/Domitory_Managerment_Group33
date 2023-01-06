using App.Shared.Repositories;
using Project.Domain.Infastructure;
using Project.Domain.Entity;

namespace Project.Infastructure.Context
{
    public class RoomRepository : RepositoryBase<Context, RoomEntity, long>, IRoomRepository
    {
        public RoomRepository(Context RoomContext)
            : base(RoomContext)
        {

        }
    }
}
