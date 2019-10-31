using Repositories.Entities;
using System.Collections.Generic;

namespace Services.Contracts
{
    public interface IBillService
    {
        Bill Get(long id);
        IEnumerable<Bill> Get(IEnumerable<long> ids);
        void Update(Bill bill);
        Bill Insert(Bill bill);
        void Delete(long id);
        IEnumerable<Bill> GetAll();
    }
}
