using App.Shared.Repositories;
using Project.Domain.Infastructure;
using Project.Domain.Entity;
using Project.Infastructure.Context.Student;
using Project.Infastructure.Context.Account;

namespace Project.Infastructure.Repositories
{
    public class AccountRepository : RepositoryBase<AccountContext, AccountEntity, long>, IAccountRepository
    {
        public AccountRepository(AccountContext accountContext)
            : base(accountContext)
        {

        }
    }
}
