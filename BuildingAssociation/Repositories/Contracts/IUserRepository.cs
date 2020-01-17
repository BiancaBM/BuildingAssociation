using Repositories.Entities;
using System.Collections.Generic;

namespace Repositories.Contracts
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User GetByCredentials(string email, string password);
    }
}
