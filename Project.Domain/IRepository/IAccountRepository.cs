﻿using AppShared.Repositories;
using Project.Domain.Entity;

namespace Project.Domain.Infastructure
{
    public interface IAccountRepository : IRepository<AccountEntity, long>
    {
    }
}
