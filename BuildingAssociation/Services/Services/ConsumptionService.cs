using System.Collections.Generic;
using Repositories.Contracts;
using Repositories.Entities;
using Services.Contracts;

namespace Services.Services
{
    public class ConsumptionService : IConsumptionService
    {
        private IConsumptionRepository _consumptionRepository;

        public ConsumptionService(IConsumptionRepository consumptionRepository)
        {
            _consumptionRepository = consumptionRepository;
        }

        public void Delete(long id)
        {
            _consumptionRepository.Delete(id);
        }

        public Consumption Get(long id)
        {
            return _consumptionRepository.Get(id);
        }

        public IEnumerable<Consumption> Get(IEnumerable<long> ids)
        {
            return _consumptionRepository.Get(ids);
        }

        public IEnumerable<Consumption> GetAll()
        {
            return _consumptionRepository.GetAll();
        }

        public Consumption Insert(Consumption consumption)
        {
            return _consumptionRepository.Insert(consumption);
        }

        public void Update(Consumption consumption)
        {
            _consumptionRepository.Update(consumption);
        }
    }
}
