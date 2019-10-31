using Repositories.Entities;
using System.Collections.Generic;

namespace Services.Contracts
{
    public interface IUserService
    {
        User Insert(User user);
        User Get(long id);
        IEnumerable<User> Get(IEnumerable<long> ids);
        void Update(User user);
        void Delete(long id);
        IEnumerable<User> GetAll();
    }
}
