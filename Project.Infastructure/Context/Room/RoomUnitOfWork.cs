using App.Shared.Uow;

namespace Project.Infastructure.Context.Room
{
    public class RoomUnitOfWork : UnitOfWorkBase<RoomContext>, IMaxUnitOfWork
    {
        public RoomUnitOfWork(RoomContext modelContext) : base(modelContext)
        {
        }
    }
}
