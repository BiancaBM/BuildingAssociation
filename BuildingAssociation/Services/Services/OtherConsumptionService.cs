using System.Collections.Generic;
using Repositories.Contracts;
using Repositories.Entities;
using Services.Contracts;

namespace Services.Services
{
    public class OtherConsumptionService : IOtherConsumptionService
    {
        private IOtherConsumptionRepository _repository;

        public OtherConsumptionService(IOtherConsumptionRepository repo)
        {
            _repository = repo;
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public OtherConsumption Get(long id)
        {
            return _repository.Get(id);
        }

        public IEnumerable<OtherConsumption> Get(IEnumerable<long> ids)
        {
            return _repository.Get(ids);
        }

        public IEnumerable<OtherConsumption> GetAll()
        {
            return _repository.GetAll();
        }

        public OtherConsumption Insert(OtherConsumption item)
        {
            return _repository.Insert(item);
        }

        public void Update(OtherConsumption item)
        {
            _repository.Update(item);
        }
    }
}
