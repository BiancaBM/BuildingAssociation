using Repositories.Contracts;
using Repositories.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Repositories.Repositories
{
    public class ProviderBillRepository :  IProviderBillRepository
    {
        private BuildingAssociationContext _ctx;
        private DbSet<ProviderBill> ProviderBills { get; set; }

        public ProviderBillRepository(BuildingAssociationContext context)
        {
            _ctx = context;
            ProviderBills = context.Bills;
        }

        public void Delete(long id)
        {
            var billToBeRemoved = ProviderBills.FirstOrDefault(x => x.UniqueId == id);
            ProviderBills.Remove(billToBeRemoved);

            _ctx.SaveChanges();
        }

        public ProviderBill Get(long id)
        {
            return ProviderBills.FirstOrDefault(x => x.UniqueId == id);
        }

        public IEnumerable<ProviderBill> Get(IEnumerable<long> ids)
        {
            return ProviderBills.Where(bill => ids.Any(id => id == bill.UniqueId)).ToList();
        }

        public IEnumerable<ProviderBill> GetAll()
        {
            return ProviderBills.ToList();
        }

        public ProviderBill Insert(ProviderBill bill)
        {
            var insertedBill = ProviderBills.Add(bill);
            _ctx.SaveChanges();

            return insertedBill;
        }

        public void Update(ProviderBill bill)
        {
            var updatedBill = ProviderBills.FirstOrDefault(x => x.UniqueId == bill.UniqueId);
            updatedBill.MansionId = bill.MansionId;
            updatedBill.ProviderId = bill.ProviderId;
            updatedBill.Units = bill.Units;
            updatedBill.Other = bill.Other;
            updatedBill.Paid = bill.Paid;
            updatedBill.CreationDate = bill.CreationDate;
            updatedBill.DueDate = bill.DueDate;

            _ctx.SaveChanges();
        }
    }
}

