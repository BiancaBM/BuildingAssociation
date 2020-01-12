using Repositories.Contracts;
using Repositories.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Repositories.Repositories
{
    public class ProviderRepository : IProviderRepository
    {
        private BuildingAssociationContext _ctx;
        private DbSet<Provider> Providers { get; set; }

        public ProviderRepository(BuildingAssociationContext context)
        {
            _ctx = context;
            Providers = context.Providers;
        }

        public void Delete(long id)
        {
            var providerToBeRemoved = Providers.FirstOrDefault(x => x.UniqueId == id);
            Providers.Remove(providerToBeRemoved);

            _ctx.SaveChanges();
        }

        public Provider Get(long id)
        {
            return Providers.FirstOrDefault(x => x.UniqueId == id);
        }

        public IEnumerable<Provider> Get(IEnumerable<long> ids)
        {
            return Providers.Where(provider => ids.Any(id => id == provider.UniqueId)).ToList();
        }

        public IEnumerable<Provider> GetAll()
        {
            return Providers.ToList();
        }

        public Provider Insert(Provider provider)
        {
            var insertedProvider = Providers.Add(provider);
            _ctx.SaveChanges();

            return insertedProvider;
        }

        public void Update(Provider provider)
        {
            var updatedProvider = Providers.FirstOrDefault(x => x.UniqueId == provider.UniqueId);
            updatedProvider.Name = provider.Name;
            updatedProvider.BankAccount = provider.BankAccount;
            updatedProvider.CUI = provider.CUI;
            updatedProvider.UnitPrice = provider.UnitPrice;

            _ctx.SaveChanges();
        }
    }
}
