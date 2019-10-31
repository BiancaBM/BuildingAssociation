using Repositories.Entities;
using System.Collections.Generic;

namespace Repositories.Contracts
{
    public interface IProviderRepository
    {
        Provider Get(long id);
        IEnumerable<Provider> Get(IEnumerable<long> ids);
        void Update(Provider provider);
        Provider Insert(Provider provider);
        void Delete(long id);
        IEnumerable<Provider> GetAll();
    }
}
