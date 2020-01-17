using Repositories.Entities;
using System.Collections.Generic;

namespace Services.Contracts
{
    public interface IUserService : IBaseService<User>
    {
        User GetByCredentials(string email, string password);
    }
}
