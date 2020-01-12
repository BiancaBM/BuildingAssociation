using System.Collections.Generic;
using Repositories.Contracts;
using Repositories.Entities;
using Services.Contracts;

namespace Services.Services
{
    public class WaterConsumptionService : IWaterConsumptionService
    {
        private IWaterConsumptionRepository _consumptionRepository;

        public WaterConsumptionService(IWaterConsumptionRepository consumptionRepository)
        {
            _consumptionRepository = consumptionRepository;
        }

        public void Delete(long id)
        {
            _consumptionRepository.Delete(id);
        }

        public WaterConsumption Get(long id)
        {
            return _consumptionRepository.Get(id);
        }

        public IEnumerable<WaterConsumption> Get(IEnumerable<long> ids)
        {
            return _consumptionRepository.Get(ids);
        }

        public IEnumerable<WaterConsumption> GetAll()
        {
            return _consumptionRepository.GetAll();
        }

        public WaterConsumption Insert(WaterConsumption consumption)
        {
            return _consumptionRepository.Insert(consumption);
        }

        public void Update(WaterConsumption consumption)
        {
            _consumptionRepository.Update(consumption);
        }
    }
}
