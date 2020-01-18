namespace Repositories.Migrations
{
    using global::Repositories.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

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

            Apartment apartment1 = new Apartment
            {
                UniqueId = apartmentId1,
                Surface = 50,
                Number = 25,
                Floor = 7,
                UserId = userId2

            };

            context.Apartments.AddOrUpdate(new[] { apartment1 });

            User user1 = new User
            {
                IsAdmin = true,
                DateCreated = DateTime.UtcNow,
                Email = "admin@buildingassociation.com",
                MembersCount = 0,
                Name = "Bianca Morar",
                Password = "YWJjZDEyMzQ=",
                UniqueId = userId1,
            };

            User user2 = new User
            {
                IsAdmin = false,
                DateCreated = DateTime.UtcNow,
                Email = "user2@buildingassociation.com",
                MembersCount = 2,
                Name = "User Doi",
                Password = "YWJjZDEyMzQ=",
                UniqueId = userId2,
                Apartments = new[] { apartment1 }, // aici poti observa ca am facut eu legatura, si entity framework va sti in ce tabel sa puna ;)
            };

            context.Users.AddOrUpdate(new[] { user1, user2 });


            Provider provider1 = new Provider
            {
                UniqueId = providerId1,
                Name = "Apa",
                UnitPrice = 7.13,
                BankAccount = "RO112213244231", //max14
                CUI = "APA12132000" //max11
            };

            Provider provider2 = new Provider
            {
                UniqueId = providerId2,
                Name = "Electrica",
                UnitPrice = 0.7,
                BankAccount = "RO112213244678",
                CUI = "ELECTRICA12"
            };

            context.Providers.AddOrUpdate(new[] { provider1, provider2 });
        }
    }
}
