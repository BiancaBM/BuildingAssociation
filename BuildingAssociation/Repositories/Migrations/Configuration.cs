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

            int userId1 = 1;
            int providerId1 = 1, providerId2 = 2;

            User user1 = new User
            {
                Roles = "Admin",
                DateCreated = DateTime.UtcNow,
                Email = "admin@buildingassociation.com",
                MembersCount = 0,
                Name = "Bianca Morar",
                Password = "YWJjZDEyMzQ=",
                UniqueId = userId1,
                MansionId = 1,
            };

            context.Users.AddOrUpdate(new[] { user1 });


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
