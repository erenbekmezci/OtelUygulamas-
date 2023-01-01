using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IFoodCategoryDal : IRepository<FoodCategory>
    {
        FoodCategory GetFoodsTheCategory(string categoryName);

        FoodCategory GetByIdWithFoods(int id);

        List<FoodCategory> getYemekMenu();

    }
}
