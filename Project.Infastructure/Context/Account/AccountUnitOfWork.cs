using App.Shared.Uow;
using Project.Infastructure.Context.Account;

namespace Project.Infastructure.Context.Student
{
    public class AccountUnitOfWork : UnitOfWorkBase<AccountContext>, IMaxUnitOfWork
    {
        public AccountUnitOfWork(AccountContext modelContext) : base(modelContext)
        {
        }
    }
}
