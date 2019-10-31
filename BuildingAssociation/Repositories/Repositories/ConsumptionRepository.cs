using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Repositories.Contracts;
using Repositories.Entities;

namespace Repositories.Repositories
{
    public class ConsumptionRepository : IConsumptionRepository
    {

        private BuildingAssociationContext _ctx;
        private DbSet<Consumption> Consumptions { get; set; }

        public ConsumptionRepository(BuildingAssociationContext context)
        {
            _ctx = context;
            Consumptions = context.Consumptions;
        }

        public void Delete(long id)
        {
            var consumptionToBeRemove = Consumptions.FirstOrDefault(x => x.ConsumptionId == id);
            Consumptions.Remove(consumptionToBeRemove);

            _ctx.SaveChanges();
        }

        public Consumption Get(long id)
        {
            return Consumptions.FirstOrDefault(x => x.ConsumptionId == id);
        }

        public IEnumerable<Consumption> Get(IEnumerable<long> ids)
        {
            return Consumptions.Where(consumption => ids.Any(id => id == consumption.ConsumptionId)).ToList();
        }

        public IEnumerable<Consumption> GetAll()
        {
            return Consumptions.ToList();
        }

        public Consumption Insert(Consumption consumption)
        {
            var insertedConsumption = Consumptions.Add(consumption);

            _ctx.SaveChanges();

            return insertedConsumption;
        }

        public void Update(Consumption consumption)
        {
            var updatedConsumption = Consumptions.FirstOrDefault(x => x.ConsumptionId == consumption.ConsumptionId);
            updatedConsumption.Guid = consumption.Guid;
            updatedConsumption.Month = consumption.Month;
            updatedConsumption.Year = consumption.Year;
            updatedConsumption.Units = consumption.Units;
            updatedConsumption.Paid = consumption.Paid;
            updatedConsumption.ProviderId = consumption.ProviderId;
            updatedConsumption.UserId = consumption.UserId;
            updatedConsumption.CreationDate = consumption.CreationDate;

            _ctx.SaveChanges();

        }
    }
}
