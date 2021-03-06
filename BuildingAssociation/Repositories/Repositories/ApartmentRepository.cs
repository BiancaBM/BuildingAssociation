﻿using System;
using System.Collections.Generic;
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
            var apartmentToBeRemoved = Apartments.FirstOrDefault(x => x.UniqueId == id);
            Apartments.Remove(apartmentToBeRemoved);

            _ctx.SaveChanges();
        }

        public Apartment Get(long id)
        {
            return Apartments.FirstOrDefault(x => x.UniqueId == id);
        }

        public IEnumerable<Apartment> Get(IEnumerable<long> ids)
        {
            return Apartments.Where(apartment => ids.Any(id => id == apartment.UniqueId)).ToList();
        }

        public IEnumerable<Apartment> GetAll()
        {
            return Apartments.ToList();
        }

        public Apartment Insert(Apartment apartment)
        {
            var existApartmentWithSameNumber = Apartments.Any(x => x.MansionId == apartment.MansionId && x.Number == apartment.Number);

            if(existApartmentWithSameNumber)
            {
                throw new Exception("Exist apartment with same number!");
            }

            var insertedApartment = Apartments.Add(apartment);
            _ctx.SaveChanges();

            return insertedApartment;
        }

        public void Update(Apartment apartment)
        {
            var existApartmentWithSameNumber = Apartments.Any(x => 
                    x.UniqueId != apartment.UniqueId &&
                    x.MansionId == apartment.MansionId &&
                    x.Number == apartment.Number
                );

            if (existApartmentWithSameNumber)
            {
                throw new Exception("Already exist an apartment with the selected number!");
            }

            var updatedApartment = Apartments.FirstOrDefault(x => x.UniqueId == apartment.UniqueId);
            
            updatedApartment.Floor = apartment.Floor;
            updatedApartment.Number = apartment.Number;
            updatedApartment.Surface = apartment.Surface;
            updatedApartment.IndividualQuota = apartment.IndividualQuota;
            updatedApartment.MembersCount = apartment.MembersCount;

            _ctx.SaveChanges();
        }
    }
}
