using Repositories.Contracts;
using Repositories.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Repositories.Repositories
{
    public class OtherConsumptionRepository : IOtherConsumptionRepository
    {
        private BuildingAssociationContext _ctx;
        private DbSet<OtherConsumption> OtherConsumptions { get; set; }

        public OtherConsumptionRepository(BuildingAssociationContext context)
        {
            _ctx = context;
            OtherConsumptions = context.OtherConsumptions;
        }

        public void Delete(long id)
        {
            var toBeRemoved = OtherConsumptions.FirstOrDefault(x => x.UniqueId == id);
            OtherConsumptions.Remove(toBeRemoved);

            _ctx.SaveChanges();
        }

        public OtherConsumption Get(long id)
        {
            return OtherConsumptions.FirstOrDefault(x => x.UniqueId == id);
        }

        public IEnumerable<OtherConsumption> Get(IEnumerable<long> ids)
        {
            return OtherConsumptions.Where(item => ids.Any(id => id == item.UniqueId)).ToList();
        }

        public IEnumerable<OtherConsumption> GetAll()
        {
            return OtherConsumptions.ToList();
        }

        public OtherConsumption Insert(OtherConsumption item)
        {
            var inserted = OtherConsumptions.Add(item);
            _ctx.SaveChanges();

            return inserted;
        }

        public void Update(OtherConsumption item)
        {
            var updated = OtherConsumptions.FirstOrDefault(x => x.UniqueId == item.UniqueId);
            updated.Name = item.Name;
            updated.CalculationType = item.CalculationType;
            updated.Date = item.Date;
            updated.Price = item.Price;
            updated.MansionId = item.MansionId;

            _ctx.SaveChanges();
        }
    }
}
