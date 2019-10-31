using Repositories.Entities;
using System.Collections.Generic;

namespace Repositories.Contracts
{
    public interface IUserRepository
    {
        User Get(long id);
        IEnumerable<User> Get(IEnumerable<long> ids);
        void Update(User user);
        User Insert(User user);
        void Delete(long id);
        IEnumerable<User> GetAll();
    }
}
