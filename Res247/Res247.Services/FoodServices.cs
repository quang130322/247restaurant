using Res247.Data.Infrastructure;
using Res247.Models.Common;
using Res247.Services.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Res247.Services
{
    public class FoodServices : BaseServices<Food>, IFoodServices
    {
        public FoodServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IEnumerable<Food> GetFoodsByCate(int cateId, bool isIncludeDelete = false)
        {
            if (!isIncludeDelete)
            {
                return _unitOfWork.FoodRepository.GetQuery().Where(x => x.Categories.Any(c => c.Id == cateId) && x.IsDeleted == isIncludeDelete).ToList();
            }
            return _unitOfWork.FoodRepository.GetQuery().Where(x=>x.Categories.Any(c=>c.Id == cateId)).ToList();
        }

        public IEnumerable<Food> GetSimilarFood(int foodId, bool isIncludeDelete = false)
        {
            var cate = _unitOfWork.CategoryRepository.GetQuery().FirstOrDefault(c => c.Foods.Any(f => f.Id == foodId));
            if (!isIncludeDelete)
            {
                return _unitOfWork.FoodRepository.GetQuery().Where(x =>
                                                               x.Categories.Any(c => c.Id == cate.Id)
                                                               && x.Id != foodId && x.IsDeleted == isIncludeDelete).ToList().Take(10);
            }
            return _unitOfWork.FoodRepository.GetQuery().Where(x => 
                                                               x.Categories.Any(c => c.Id == cate.Id) 
                                                               && x.Id != foodId).ToList().Take(10);
        }

        public void RemoveCategoriesFromFood(int foodId)
        {
            var food = _unitOfWork.FoodRepository.GetById(foodId);
            List<Category> categories = new List<Category>();
            food.Categories = categories;
            _unitOfWork.FoodRepository.Update(food);
        }
    }
}
