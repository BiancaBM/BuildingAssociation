using Repositories.Contracts;
using Repositories.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Repositories.Repositories
{
    public class ConsumptionTypeRepository : IConsumptionTypeRepository
    {
        private BuildingAssociationContext _ctx;
        private DbSet<ConsumptionType> ConsumptionTypes { get; set; }

        public ConsumptionTypeRepository(BuildingAssociationContext context)
        {
            _ctx = context;
            ConsumptionTypes = context.ConsumptionTypes;
        }

        public void Delete(long id)
        {
            var toBeRemoved = ConsumptionTypes.FirstOrDefault(x => x.UniqueId == id);
            ConsumptionTypes.Remove(toBeRemoved);

            _ctx.SaveChanges();
        }

        public ConsumptionType Get(long id)
        {
            return ConsumptionTypes.FirstOrDefault(x => x.UniqueId == id);
        }

        public IEnumerable<ConsumptionType> Get(IEnumerable<long> ids)
        {
            return ConsumptionTypes.Where(item => ids.Any(id => id == item.UniqueId)).ToList();
        }

        public IEnumerable<ConsumptionType> GetAll()
        {
            return ConsumptionTypes.ToList();
        }

        public ConsumptionType Insert(ConsumptionType item)
        {
            var inserted = ConsumptionTypes.Add(item);
            _ctx.SaveChanges();

            return inserted;
        }

        public void Update(ConsumptionType item)
        {
            var updated = ConsumptionTypes.FirstOrDefault(x => x.UniqueId == item.UniqueId);
            updated.Name = item.Name;
            updated.CalculationType = item.CalculationType;
            updated.Date = item.Date;
            _ctx.SaveChanges();
        }
    }
}
