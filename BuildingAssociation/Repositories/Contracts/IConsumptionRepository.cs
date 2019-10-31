using Repositories.Entities;
using System.Collections.Generic;

namespace Repositories.Contracts
{
    public interface IConsumptionRepository
    {
        Consumption Get(long id);
        IEnumerable<Consumption> Get(IEnumerable<long> ids);
        void Update(Consumption consumption);
        Consumption Insert(Consumption consumption);
        void Delete(long id);
        IEnumerable<Consumption> GetAll();
    }
}
