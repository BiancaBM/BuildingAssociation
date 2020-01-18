using Repositories.Entities;
using System.Configuration;
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
        
        public BuildingAssociationContext() : base("BuildingAssociation")
        {
            this.Configuration.ProxyCreationEnabled = true;
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.AutoDetectChangesEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //https://docs.microsoft.com/en-us/ef/ef6/modeling/code-first/fluent/relationships
        //    modelBuilder.Entity<User>()
        //        .Property(u => u.Guid)
        //        .HasColumnName("UniqueIdentifier");
        }

    }
}
