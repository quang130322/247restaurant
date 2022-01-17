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
            modelBuilder.Entity<CovidInfo>().HasOptional(c=>c.Shipper).WithRequired(a=>a.CovidInfo);
            modelBuilder.Entity<CovidInfo>().HasOptional(c=>c.Customer).WithRequired(a=>a.CovidInfo);
        }

        public override int SaveChanges()
        {
            BeforeSaveChanges();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync()
        {
            BeforeSaveChanges();
            return await base.SaveChangesAsync();
        }

        private void BeforeSaveChanges()
        {
            var entities = this.ChangeTracker.Entries();
            foreach (var entry in entities)
            {
                if (entry.Entity is IBaseEntity entityBase)
                {
                    switch (entry.State)
                    {
                        case EntityState.Modified: entityBase.UpdatedAt = DateTime.Now; break;
                        case EntityState.Added:
                            entityBase.UpdatedAt = DateTime.Now;
                            entityBase.CreatedAt = DateTime.Now;
                            break;
                    }
                }
            }
        }
    }
}
