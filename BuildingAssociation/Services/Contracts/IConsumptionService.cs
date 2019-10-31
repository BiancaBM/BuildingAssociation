using Repositories.Entities;
using System.Collections.Generic;

namespace Services.Contracts
{
    public interface IConsumptionService
    {
        Consumption Get(long id);
        IEnumerable<Consumption> Get(IEnumerable<long> ids);
        void Update(Consumption consumption);
        Consumption Insert(Consumption consumption);
        void Delete(long id);
        IEnumerable<Consumption> GetAll();
    }
}
