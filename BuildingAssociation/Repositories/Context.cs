using Repositories.Entities;
using System.Data.Entity;

namespace Repositories
{
    public class BuildingAssociationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<ProviderBill> Bills { get; set; }
        public DbSet<UserBillItem> UserBillItems { get; set; }
        public DbSet<WaterConsumption> WaterConsumptions { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<ConsumptionType> ConsumptionTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //aici un exemplu de proprietate denumita Guid ce in baza de date se va numi ReferenceCode,
            //poti pastra asta, ca sa ai la indemana exemplul cu FluentApi
            // citeste aici sa vezi cum mai poti crea tabele, fain, folosind FluentApi
            //https://docs.microsoft.com/en-us/ef/ef6/modeling/code-first/fluent/relationships
        //    modelBuilder.Entity<User>()
        //        .Property(u => u.Guid)
        //        .HasColumnName("UniqueIdentifier");
        }

    }
}
