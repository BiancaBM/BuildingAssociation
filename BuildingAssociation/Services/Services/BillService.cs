using System.Collections.Generic;
using Repositories.Contracts;
using Repositories.Entities;
using Services.Contracts;

namespace Services.Services
{
    public class BillService : IBillService
    {
        private IBillRepository _billRepository;

        public BillService(IBillRepository billRepository)
        {
            _billRepository = billRepository;  
        }

        public void Delete(long id)
        {
            _billRepository.Delete(id);
        }

        public Bill Get(long id)
        {
            return _billRepository.Get(id);
        }

        public IEnumerable<Bill> Get(IEnumerable<long> ids)
        {
            return _billRepository.Get(ids);
        }

        public IEnumerable<Bill> GetAll()
        {
            return _billRepository.GetAll();
        }

        public Bill Insert(Bill bill)
        {
            return _billRepository.Insert(bill);
        }

        public void Update(Bill bill)
        {
            _billRepository.Update(bill);
        }
    }
}
