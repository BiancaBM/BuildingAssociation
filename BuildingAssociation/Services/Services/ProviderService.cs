using System.Collections.Generic;
using Repositories.Contracts;
using Repositories.Entities;
using Services.Contracts;

namespace Services.Services
{
    public class ProviderService : IProviderService
    {
        private IProviderRepository _providerRepository;

        public ProviderService(IProviderRepository providerRepository)
        {
            _providerRepository = providerRepository;
        }

        public void Delete(long id)
        {
            _providerRepository.Delete(id);
        }

        public Provider Get(long id)
        {
            return _providerRepository.Get(id);
        }

        public IEnumerable<Provider> Get(IEnumerable<long> ids)
        {
            return _providerRepository.Get(ids);
        }

        public IEnumerable<Provider> GetAll()
        {
            return _providerRepository.GetAll();
        }

        public Provider Insert(Provider provider)
        {
            return _providerRepository.Insert(provider);
        }

        public void Update(Provider provider)
        {
            _providerRepository.Update(provider);
        }
    }
}
