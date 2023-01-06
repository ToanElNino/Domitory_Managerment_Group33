using App.Shared.Repositories;
using Project.Domain.Infastructure;
using Project.Domain.Entity;

namespace Project.Infastructure.Context
{
    public class AccountRepository : RepositoryBase<Context, AccountEntity, long>, IAccountRepository
    {
        public AccountRepository(Context accountContext)
            : base(accountContext)
        {

        }
    }
}
