namespace Repositories.Migrations
{
    using Entities;
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<BuildingAssociationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BuildingAssociationContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            int apartmentId1 = 1;
            int userId1 = 1, userId2 = 2;
            int billId1 = 1, billId2 = 2;
            int providerId1 = 1, providerId2 = 2;
            int consumptionId1 = 1, consumptionId2 = 2;

            Apartment apartment1 = new Apartment
            {
                ApartmentId = apartmentId1,
                Surface = 50,
                Number = 25,
                Floor = 7,
                
            };

            context.Apartments.AddOrUpdate(new[] { apartment1 });

            User user1 = new User
            {
                Guid = Guid.NewGuid(),
                IsAdmin = true,
                DateCreated = DateTime.UtcNow,
                Email = "admin@buildingassociation.com",
                MembersCount = 0,
                Name = "Bianca Morar",
                Password = "abcd1234",
                UserId = userId1,
            };

            User user2 = new User
            {
                Guid = Guid.NewGuid(),
                IsAdmin = false,
                DateCreated = DateTime.UtcNow,
                Email = "user2@buildingassociation.com",
                MembersCount = 2,
                Name = "User Doi",
                Password = "abcd1234",
                UserId = userId2,
                Apartments = new[] { apartment1 } // aici poti observa ca am facut eu legatura, si entity framework va sti in ce tabel sa puna ;)
            };

            context.Users.AddOrUpdate(new[] { user1, user2 });


            Provider provider1 = new Provider
            {
                Guid = Guid.NewGuid(),
                ProviderId = providerId1,
                Name = "Apa",
                UnitPrice = 7.13,
                BankAccount = "RO112213244231", //max14
                CUI = "APA12132000" //max11
            };

            Provider provider2 = new Provider
            {
                Guid = Guid.NewGuid(),
                ProviderId = providerId2,
                Name = "Electrica",
                UnitPrice = 0.7,
                BankAccount = "RO112213244678",
                CUI = "ELECTRICA12"
            };

            context.Providers.AddOrUpdate(new[] { provider1, provider2 });

            Bill bill1 = new Bill
            {
                Guid = Guid.NewGuid(),
                BillId = billId1,
                TotalPrice = 2345.59,
                ProviderId = providerId1,
                CreationDate = DateTime.UtcNow,
                Paid = false,
                DueDate = DateTime.UtcNow.AddDays(60),
            };

            Bill bill2 = new Bill
            {
                Guid = Guid.NewGuid(),
                BillId = billId2,
                TotalPrice = 345.59,
                ProviderId = providerId2,
                Provider = provider2,
                CreationDate = DateTime.UtcNow,
                Paid = false,
                DueDate = DateTime.UtcNow.AddDays(90)
            };

            context.Bills.AddOrUpdate(new[] { bill1, bill2 });

            Consumption consWater = new Consumption
            {
                Guid = Guid.NewGuid(),
                ConsumptionId = consumptionId1,
                ProviderId = providerId1,
                Year = DateTime.UtcNow.Year,
                Month = DateTime.UtcNow.Month-1,
                Paid = true,
                CreationDate = DateTime.UtcNow,
                Units = 5,
                UserId = userId2,
            };

            Consumption consElectrical = new Consumption
            {
                Guid = Guid.NewGuid(),
                ConsumptionId = consumptionId2,
                ProviderId = providerId2,
                Year = DateTime.UtcNow.Year,
                Month = DateTime.UtcNow.Month - 1,
                Paid = false,
                CreationDate = DateTime.UtcNow,
                Units = 100,
                UserId = userId2,

            };

            context.Consumptions.AddOrUpdate(new[] { consWater, consElectrical });
        }
    }
}
