using Repositories.Entities;
using System.Collections.Generic;

namespace Services.Contracts
{
    public interface IApartmentService
    {
        Apartment Get(long id);
        IEnumerable<Apartment> Get(IEnumerable<long> ids);
        void Update(Apartment apartment);
        Apartment Insert(Apartment apartment);
        void Delete(long id);
        IEnumerable<Apartment> GetAll();
    }
}
