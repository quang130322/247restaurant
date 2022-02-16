using Res247.Models.BaseEntities;
using Res247.Models.Common;
using Res247.Models.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Res247.Data
{
    public class Res247Context : DbContext
    {
        public Res247Context() : base("Res247Cnn")
        {
            Database.SetInitializer<Res247Context>(new DbInitializer());
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Shipper> Shippers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<CovidInfo> CovidInfos { get; set; }
        public DbSet<ShipperOrder> ShipperOrders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Food>()
                .HasMany(f => f.Categories)
                .WithMany(c => c.Foods)
                .Map(m =>
                {
                    m.MapLeftKey("FoodId");
                    m.MapRightKey("CateId");
                    m.ToTable("FoodCategories", "common");
                });

            modelBuilder.Entity<Customer>().HasOptional(c=>c.Account).WithRequired(a=>a.Customer);
            modelBuilder.Entity<Shipper>().HasOptional(c=>c.Account).WithRequired(a=>a.Shipper);
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
