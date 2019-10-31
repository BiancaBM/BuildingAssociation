using Repositories.Entities;
using System.Collections.Generic;

namespace Repositories.Contracts
{
    public interface IApartmentRepository
    {
        Apartment Get(long id);
        IEnumerable<Apartment> Get(IEnumerable<long> ids);
        void Update(Apartment apartment);
        Apartment Insert(Apartment apartment);
        void Delete(long id);
        IEnumerable<Apartment> GetAll();
    }
}
