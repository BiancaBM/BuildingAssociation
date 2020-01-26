using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Repositories.Contracts;
using Repositories.Entities;

namespace Repositories.Repositories
{
    public class WaterConsumptionRepository : IWaterConsumptionRepository
    {

        private BuildingAssociationContext _ctx;
        private DbSet<WaterConsumption> WaterConsumptions { get; set; }

        public WaterConsumptionRepository(BuildingAssociationContext context)
        {
            _ctx = context;
            WaterConsumptions = context.WaterConsumptions;
        }

        public void Delete(long id)
        {
            var consumptionToBeRemove = WaterConsumptions.FirstOrDefault(x => x.UniqueId == id);
            WaterConsumptions.Remove(consumptionToBeRemove);

            _ctx.SaveChanges();
        }

        public WaterConsumption Get(long id)
        {
            return WaterConsumptions.FirstOrDefault(x => x.UniqueId == id);
        }

        public IEnumerable<WaterConsumption> Get(IEnumerable<long> ids)
        {
            return WaterConsumptions.Where(consumption => ids.Any(id => id == consumption.UniqueId)).ToList();
        }

        public IEnumerable<WaterConsumption> GetAll()
        {
            return WaterConsumptions.ToList();
        }

        public WaterConsumption Insert(WaterConsumption consumption)
        {
            var insertedConsumption = WaterConsumptions.Add(consumption);

            _ctx.SaveChanges();

            return insertedConsumption;
        }

        public void Update(WaterConsumption consumption)
        {
            var updatedConsumption = WaterConsumptions.FirstOrDefault(x => x.UniqueId == consumption.UniqueId);
            updatedConsumption.KitchenUnits = consumption.KitchenUnits;
            updatedConsumption.BathroomUnits = consumption.BathroomUnits;
            updatedConsumption.UserId = consumption.UserId;
            updatedConsumption.CreationDate = consumption.CreationDate;

            _ctx.SaveChanges();

        }
    }
}
