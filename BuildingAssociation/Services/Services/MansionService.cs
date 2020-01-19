using System.Collections.Generic;
using Repositories.Contracts;
using Repositories.Entities;
using Services.Contracts;

namespace Services.Services
{
    public class MansionService : IMansionService
    {
        private IMansionRepository _mansionRepository;

        public MansionService(IMansionRepository repo)
        {
            _mansionRepository = repo;
        }

        public void Delete(long id)
        {
            _mansionRepository.Delete(id);
        }

        public Mansion Get(long id)
        {
            return _mansionRepository.Get(id);
        }

        public IEnumerable<Mansion> Get(IEnumerable<long> ids)
        {
            return _mansionRepository.Get(ids);
        }

        public IEnumerable<Mansion> GetAll()
        {
            return _mansionRepository.GetAll();
        }

        public Mansion Insert(Mansion item)
        {
            return _mansionRepository.Insert(item);
        }

        public void Update(Mansion item)
        {
            _mansionRepository.Update(item);
        }
    }
}
