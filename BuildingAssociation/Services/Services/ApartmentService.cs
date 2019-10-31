using System.Collections.Generic;
using Repositories.Contracts;
using Repositories.Entities;
using Services.Contracts;

namespace Services.Services
{
    public class ApartmentService : IApartmentService
    {
        private IApartmentRepository _apartmentRepository;

        public ApartmentService(IApartmentRepository apartmentService)
        {
            _apartmentRepository = apartmentService;
        }

        public void Delete(long id)
        {
            _apartmentRepository.Delete(id);
        }

        public Apartment Get(long id)
        {
            return _apartmentRepository.Get(id);
        }

        public IEnumerable<Apartment> Get(IEnumerable<long> ids)
        {
            return _apartmentRepository.Get(ids);
        }

        public IEnumerable<Apartment> GetAll()
        {
            return _apartmentRepository.GetAll();
        }

        public Apartment Insert(Apartment apartment)
        {
            return _apartmentRepository.Insert(apartment);
        }

        public void Update(Apartment apartment)
        {
            _apartmentRepository.Update(apartment);
        }
    }
}
