using Repositories.Contracts;
using Repositories.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Repositories.Repositories
{
    public class BillRepository :  IBillRepository
    {
        private BuildingAssociationContext _ctx;
        private DbSet<Bill> Bills { get; set; }

        public BillRepository(BuildingAssociationContext context)
        {
            _ctx = context;
            Bills = context.Bills;
        }

        public void Delete(long id)
        {
            var billToBeRemoved = Bills.FirstOrDefault(x => x.BillId == id);
            Bills.Remove(billToBeRemoved);

            _ctx.SaveChanges();
        }

        public Bill Get(long id)
        {
            return Bills.FirstOrDefault(x => x.BillId == id);
        }

        public IEnumerable<Bill> Get(IEnumerable<long> ids)
        {
            return Bills.Where(bill => ids.Any(id => id == bill.BillId)).ToList();
        }

        public IEnumerable<Bill> GetAll()
        {
            return Bills.ToList();
        }

        public Bill Insert(Bill bill)
        {
            var insertedBill = Bills.Add(bill);
            _ctx.SaveChanges();

            return insertedBill;
        }

        public void Update(Bill bill)
        {
            var updatedBill = Bills.FirstOrDefault(x => x.BillId == bill.BillId);
            updatedBill.Guid = bill.Guid;
            updatedBill.CreationDate = bill.CreationDate;
            updatedBill.DueDate = bill.DueDate;
            updatedBill.Paid = bill.Paid;
            updatedBill.ProviderId = bill.ProviderId;
            updatedBill.TotalPrice = bill.TotalPrice;

            _ctx.SaveChanges();
        }
    }
}

