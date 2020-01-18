using System;
using System.Collections.Generic;
using Repositories.Contracts;
using Repositories.Entities;
using Services.Contracts;

namespace Services.Services
{
    public class ProviderBillService : IProviderBillService
    {
        private IProviderBillRepository _billRepository;

        public ProviderBillService(IProviderBillRepository billRepository)
        {
            _billRepository = billRepository;  
        }

        public void Delete(long id)
        {
            _billRepository.Delete(id);
        }

        public ProviderBill Get(long id)
        {
            return _billRepository.Get(id);
        }

        public IEnumerable<ProviderBill> Get(IEnumerable<long> ids)
        {
            return _billRepository.Get(ids);
        }

        public IEnumerable<ProviderBill> GetAll()
        {
            return _billRepository.GetAll();
        }

        public ProviderBill Insert(ProviderBill bill)
        {
            bill.CreationDate = DateTime.UtcNow;
            return _billRepository.Insert(bill);
        }

        public void Update(ProviderBill bill)
        {
            _billRepository.Update(bill);
        }
    }
}
