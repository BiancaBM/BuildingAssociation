﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Repositories.Contracts;
using Repositories.Entities;

namespace Repositories.Repositories
{
    public class ApartmentRepository : IApartmentRepository
    {

        private BuildingAssociationContext _ctx;
        private DbSet<Apartment> Apartments { get; set; }

        public ApartmentRepository(BuildingAssociationContext context)
        {
            _ctx = context;
            Apartments = context.Apartments;
        }

        public void Delete(long id)
        {
            var apartmentToBeRemoved = Apartments.FirstOrDefault(x => x.ApartmentId == id);
            Apartments.Remove(apartmentToBeRemoved);

            _ctx.SaveChanges();
        }

        public Apartment Get(long id)
        {
            return Apartments.FirstOrDefault(x => x.ApartmentId == id);
        }

        public IEnumerable<Apartment> Get(IEnumerable<long> ids)
        {
            return Apartments.Where(apartment => ids.Any(id => id == apartment.ApartmentId)).ToList();
        }

        public IEnumerable<Apartment> GetAll()
        {
            return Apartments.ToList();
        }

        public Apartment Insert(Apartment apartment)
        {
            var insertedApartment = Apartments.Add(apartment);
            _ctx.SaveChanges();

            return insertedApartment;
        }

        public void Update(Apartment apartment)
        {
            var updatedApartment = Apartments.FirstOrDefault(x => x.ApartmentId == apartment.ApartmentId);
            updatedApartment.Floor = apartment.Floor;
            updatedApartment.Guid = apartment.Guid;
            updatedApartment.Number = apartment.Number;
            updatedApartment.Surface = apartment.Surface;
            updatedApartment.UserId = apartment.UserId;

            _ctx.SaveChanges();
        }
    }
}