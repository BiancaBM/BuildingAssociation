using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Repositories.Contracts;
using Repositories.Entities;

namespace Repositories.Repositories
{
    public class MansionRepository : IMansionRepository
    {

        private BuildingAssociationContext _ctx;
        private DbSet<Mansion> Mansions { get; set; }

        public MansionRepository(BuildingAssociationContext context)
        {
            _ctx = context;
            Mansions = context.Mansions;
        }

        public void Delete(long id)
        {
            var toBeRemove = Mansions.FirstOrDefault(x => x.UniqueId == id);
            Mansions.Remove(toBeRemove);

            _ctx.SaveChanges();
        }

        public Mansion Get(long id)
        {
            return Mansions.FirstOrDefault(x => x.UniqueId == id);
        }

        public IEnumerable<Mansion> Get(IEnumerable<long> ids)
        {
            return Mansions.Where(item => ids.Any(id => id == item.UniqueId)).ToList();
        }

        public IEnumerable<Mansion> GetAll()
        {
            return Mansions.ToList();
        }

        public Mansion Insert(Mansion item)
        {
            var inserted = Mansions.Add(item);

            _ctx.SaveChanges();

            return inserted;
        }

        public void Update(Mansion item)
        {
            var updated = Mansions.FirstOrDefault(x => x.UniqueId == item.UniqueId);
            updated.Address = item.Address;
            updated.TotalFunds = item.TotalFunds;

            _ctx.SaveChanges();

        }
    }
}
