using Res247.Data.Infrastructure;
using Res247.Models.Common;
using Res247.Services.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Res247.Services
{
    public class CategoryServices : BaseServices<Category>, ICategoryService
    {
        public CategoryServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public List<int> GetCategoriesIdByFood(int id, bool isIncludeDelete = false)
        {
            List<int> categoriesId = new List<int>();
            if (!isIncludeDelete)
            {
                var categories = _unitOfWork.CategoryRepository.GetQuery(x => x.Foods.Any(f => f.Id == id && f.IsDeleted == isIncludeDelete)).ToList();
                foreach (var item in categories)
                {
                    categoriesId.Add(item.Id);
                }
                return categoriesId;
            }
            var list = _unitOfWork.CategoryRepository.GetQuery(x=>x.Foods.Any(f=>f.Id == id)).ToList();
            foreach (var item in list)
            {
                categoriesId.Add(item.Id);
            }
            return categoriesId;
        }
    }
}
