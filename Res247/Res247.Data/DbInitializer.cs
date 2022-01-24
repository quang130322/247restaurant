using Res247.Models.Common;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Res247.Data
{
    internal class DbInitializer : DropCreateDatabaseIfModelChanges<Res247Context>
    {
        protected override void Seed(Res247Context context)
        {
            var food1 = new Food()
            {
                Id = 1,
                Name = "Gimbab Chiên",
                Description = "",
                Price = 35000,
                IsDeleted = false
            };
            
            var food2 = new Food()
            {
                Id = 2,
                Name = "Gimbab Cuốn",
                Description = "",
                Price = 30000,
                IsDeleted = false
            };

            var food3 = new Food()
            {
                Id = 3,
                Name = "Mỳ Đen",
                Description = "",
                Price = 55000,
                IsDeleted = false
            };

            var food4 = new Food()
            {
                Id = 4,
                Name = "Gà Chiên Sốt Ngọt Phomai",
                Description = "",
                Price = 75000,
                IsDeleted = false
            };

            var food5 = new Food()
            {
                Id = 5,
                Name = "Mỳ Gà Tok Cay Đặc Biệt",
                Description = "",
                Price = 65000,
                IsDeleted = false
            };

            var food6 = new Food()
            {
                Id = 6,
                Name = "Mỳ Ý Sốt Bò Băm + Phomai",
                Description = "",
                Price = 50000,
                IsDeleted = false
            };

            var food7 = new Food()
            {
                Id = 7,
                Name = "Mỳ Lườn Ngỗng",
                Description = "",
                Price = 50000,
                IsDeleted = false
            };

            var food8 = new Food()
            {
                Id = 8,
                Name = "Phở Xào Bò",
                Description = "",
                Price = 50000,
                IsDeleted = false
            };

            var food9 = new Food()
            {
                Id = 9,
                Name = "Thịt Xiên Nướng",
                Description = "",
                Price = 15000,
                IsDeleted = false
            };

            var food10 = new Food()
            {
                Id = 10,
                Name = "Gà Popcorn Lắc Phomai",
                Description = "",
                Price = 50000,
                IsDeleted = false
            };

            var food11 = new Food()
            {
                Id = 11,
                Name = "Chân Gà Rang Muối",
                Description = "",
                Price = 60000,
                IsDeleted = false
            };

            var food12 = new Food()
            {
                Id = 12,
                Name = "Cánh Gà Bơ Tỏi",
                Description = "",
                Price = 40000,
                IsDeleted = false
            };

            var food13 = new Food()
            {
                Id = 13,
                Name = "Trà Thái",
                Description = "",
                Price = 15000,
                IsDeleted = false
            };

            var food14 = new Food()
            {
                Id = 14,
                Name = "Hồng Trà Trân Châu Trắng",
                Description = "",
                Price = 25000,
                IsDeleted = false
            };

            var food15 = new Food()
            {
                Id = 15,
                Name = "Coca",
                Description = "",
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
    }
}