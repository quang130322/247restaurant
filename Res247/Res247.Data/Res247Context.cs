using Microsoft.AspNet.Identity.EntityFramework;
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
    public class Res247Context : IdentityDbContext<Account>
    {
        public Res247Context() : base("Res247Cnn")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        static Res247Context()
        {
            Database.SetInitializer<Res247Context>(new DbInitializer());
        }

        public static Res247Context Create()
        {
            return new Res247Context();
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CovidInfo> CovidInfos { get; set; }
        public DbSet<Shipper> Shippers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(i=>i.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(i=>i.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(i=> new {i.RoleId, i.UserId});

            modelBuilder.Entity<Account>().HasOptional(a => a.Shipper).WithRequired(s => s.Account);
        }
    }
}
