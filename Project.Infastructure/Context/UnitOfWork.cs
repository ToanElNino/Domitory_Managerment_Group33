using App.Shared.Uow;

namespace Project.Infastructure.Context
{
    public class UnitOfWork : UnitOfWorkBase<Context>, IMaxUnitOfWork
    {
        public UnitOfWork(Context modelContext) : base(modelContext)
        {
        }
    }
}
