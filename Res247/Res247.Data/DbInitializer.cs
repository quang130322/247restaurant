using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Res247.Models.Common;
using Res247.Models.Security;
using System.Collections.Generic;
using System.Data.Entity;

namespace Res247.Data
{
    internal class DbInitializer : DropCreateDatabaseIfModelChanges<Res247Context>
    {
        protected override void Seed(Res247Context context)
        {
            InitializeIdentity(context);

            var food1 = new Food()
            {
                Id = 1,
                Name = "Gimbab Chiên",
                Description = "",
                Image = "KimBapChien.jpg",
                Price = 35000,
                IsDeleted = false
            };
            
            var food2 = new Food()
            {
                Id = 2,
                Name = "Gimbab Cuốn",
                Description = "",
                Image = "GimBapCuon.jpg",
                Price = 30000,
                IsDeleted = false
            };

            var food3 = new Food()
            {
                Id = 3,
                Name = "Mỳ Đen",
                Description = "",
                Image = "MyDen.jpg",
                Price = 55000,
                IsDeleted = false
            };

            var food4 = new Food()
            {
                Id = 4,
                Name = "Gà Chiên Sốt Ngọt Phomai",
                Description = "",
                Image = "GaChienSotNgotPhomai.jpg",
                Price = 75000,
                IsDeleted = false
            };

            var food5 = new Food()
            {
                Id = 5,
                Name = "Mỳ Gà Tok Cay Đặc Biệt",
                Description = "",
                Image = "MyGaTokCayDacBiet.jpg",
                Price = 65000,
                IsDeleted = false
            };

            var food6 = new Food()
            {
                Id = 6,
                Name = "Mỳ Ý Sốt Bò Băm + Phomai",
                Description = "",
                Image = "MyYSotBoBamPhomai.jpg",
                Price = 50000,
                IsDeleted = false
            };

            var food7 = new Food()
            {
                Id = 7,
                Name = "Mỳ Lườn Ngỗng",
                Description = "",
                Image = "MyLuonNgong.jpg",
                Price = 50000,
                IsDeleted = false
            };

            var food8 = new Food()
            {
                Id = 8,
                Name = "Phở Xào Bò",
                Description = "",
                Image = "PhoXaoBo.jpg",
                Price = 50000,
                IsDeleted = false
            };

            var food9 = new Food()
            {
                Id = 9,
                Name = "Thịt Xiên Nướng",
                Description = "",
                Image = "ThitXienNuong.jpg",
                Price = 15000,
                IsDeleted = false
            };

            var food10 = new Food()
            {
                Id = 10,
                Name = "Gà Popcorn Lắc Phomai",
                Description = "",
                Image = "GaPopCornLacPhomai.jpg",
                Price = 50000,
                IsDeleted = false
            };

            var food11 = new Food()
            {
                Id = 11,
                Name = "Chân Gà Rang Muối",
                Description = "",
                Image = "ChanGaRangMuoi.jpg",
                Price = 60000,
                IsDeleted = false
            };

            var food12 = new Food()
            {
                Id = 12,
                Name = "Cánh Gà Bơ Tỏi",
                Description = "",
                Image = "CanhGaBoToi.jpg",
                Price = 40000,
                IsDeleted = false
            };

            var food13 = new Food()
            {
                Id = 13,
                Name = "Trà Thái",
                Description = "",
                Image = "TraThai.jpg",
                Price = 15000,
                IsDeleted = false
            };

            var food14 = new Food()
            {
                Id = 14,
                Name = "Hồng Trà Trân Châu Trắng",
                Description = "",
                Image = "HongTraTranChauTrang.jpg",
                Price = 25000,
                IsDeleted = false
            };

            var food15 = new Food()
            {
                Id = 15,
                Name = "Coca",
                Description = "",
                Image = "Coca.jpg",
                Price = 15000,
                IsDeleted = false
            };

            var categories = new List<Category>()
            {
                new Category()
                {
                    Id = 1,
                    Name = "Đồ Hàn",
                    Description="",
                    Foods = new List<Food>(){food1, food2, food3,food4,food5},
                    IsDeleted=false
                },
                new Category()
                {
                    Id = 2,
                    Name = "Đồ Uống",
                    Description="",
                    Foods = new List<Food>(){ food13,food14, food15},
                    IsDeleted=false
                },
                new Category()
                {
                    Id = 3,
                    Name = "Phở, Mỳ, Miến",
                    Description="",
                    Foods = new List<Food>(){ food3,food5,food6,food7,food8},
                    IsDeleted=false
                },
                new Category()
                {
                    Id = 4,
                    Name = "Đồ Ăn Vặt",
                    Description="",
                    Foods = new List<Food>(){ food9,food10,food11,food12},
                    IsDeleted=false
                }
            };

            context.Categories.AddRange(categories);
            context.SaveChanges();
        }

        public static void InitializeIdentity(Res247Context db)
        {
            var userManager = new UserManager<Account>(new UserStore<Account>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            const string name = "admin@example.com";
            const string password = "admin123";
            const string roleName = "Administrator";

            #region Role
            //Create Role Admin if it does not exist
            var role = roleManager.FindByName(roleName);
            if (role == null)
            {
                role = new IdentityRole(roleName);
                var roleResult = roleManager.Create(role);
            }

            var shipper = roleManager.FindByName("Shipper");
            if (shipper == null)
            {
                shipper = new IdentityRole("Shipper");
                var roleResult = roleManager.Create(shipper);
            }

            var customer = roleManager.FindByName("Customer");
            if (customer == null)
            {
                customer = new IdentityRole("Customer");
                var roleResult = roleManager.Create(customer);
            }
            #endregion

            var user = userManager.FindByName(name);
            if (user == null)
            {
                user = new Account { UserName = name, Email = name, Name = "Admin", Address="Tay Ho", PhoneNumber="0123456789"};
                var result = userManager.Create(user, password);
                result = userManager.SetLockoutEnabled(user.Id, false);
            }

            var shipAcc = new Account { UserName = "shipper@example.com", Email = "shipper@example.com", Name = "Shipper", Address = "Tay Ho", PhoneNumber = "123456789" };
            userManager.Create(shipAcc, "123");
            var ship = new Shipper { Account = shipAcc, Id = 1, Status = true };
            db.Shippers.Add(ship);


            //Add user admin to Role Admin if not already added
            var rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                var result = userManager.AddToRole(user.Id, role.Name);
            }
        }
    }


}