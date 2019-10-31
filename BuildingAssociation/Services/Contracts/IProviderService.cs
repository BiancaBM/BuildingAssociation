using Repositories.Entities;
using System.Collections.Generic;

namespace Services.Contracts
{
    public interface IProviderService
    {
        Provider Get(long id);
        IEnumerable<Provider> Get(IEnumerable<long> ids);
        void Update(Provider provider);
        Provider Insert(Provider provider);
        void Delete(long id);
        IEnumerable<Provider> GetAll();
    }
}
