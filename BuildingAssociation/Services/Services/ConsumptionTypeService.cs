using System.Collections.Generic;
using Repositories.Contracts;
using Repositories.Entities;
using Services.Contracts;

namespace Services.Services
{
    public class ConsumptionTypeService : IConsumptionTypeService
    {
        private IConsumptionTypeRepository _repository;

        public ConsumptionTypeService(IConsumptionTypeRepository repo)
        {
            _repository = repo;
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public ConsumptionType Get(long id)
        {
            return _repository.Get(id);
        }

        public IEnumerable<ConsumptionType> Get(IEnumerable<long> ids)
        {
            return _repository.Get(ids);
        }

        public IEnumerable<ConsumptionType> GetAll()
        {
            return _repository.GetAll();
        }

        public ConsumptionType Insert(ConsumptionType item)
        {
            return _repository.Insert(item);
        }

        public void Update(ConsumptionType item)
        {
            _repository.Update(item);
        }
    }
}
