using Repositories.Contracts;
using Repositories.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Repositories.Repositories
{
    public class GeneratedBillRepository : IGeneratedBillRepository
    {
        private BuildingAssociationContext _ctx;
        private DbSet<GeneratedBill> GeneratedBills { get; set; }

        public GeneratedBillRepository(BuildingAssociationContext context)
        {
            _ctx = context;
            GeneratedBills = context.GeneratedBills;
        }

        public void Delete(long id)
        {
            var toBeRemoved = GeneratedBills.FirstOrDefault(x => x.UniqueId == id);
            GeneratedBills.Remove(toBeRemoved);

            _ctx.SaveChanges();
        }

        public GeneratedBill Get(long id)
        {
            return GeneratedBills.FirstOrDefault(x => x.UniqueId == id);
        }

        public IEnumerable<GeneratedBill> Get(IEnumerable<long> ids)
        {
            return GeneratedBills.Where(x => ids.Any(id => id == x.UniqueId)).ToList();
        }

        public IEnumerable<GeneratedBill> GetAll()
        {
            return GeneratedBills.ToList();
        }

        public GeneratedBill Insert(GeneratedBill item)
        {
            var inserted = GeneratedBills.Add(item);
            _ctx.SaveChanges();

            return inserted;
        }

        public void Update(GeneratedBill item)
        {
            var updated = GeneratedBills.FirstOrDefault(x => x.UniqueId == item.UniqueId);
            updated.Date = item.Date;
            updated.CSV = item.CSV;

            _ctx.SaveChanges();
        }
    }
}
