using DataAccess.Abstract;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EfCore
{
    public class EfCoreFoodCategoryDal : EfCoreGenericRepository<FoodCategory, OtelContext>, IFoodCategoryDal
    {


        public FoodCategory GetByIdWithFoods(int id)
        {
            using (var context = new OtelContext())
            {
                return context.FoodCategories.Where(i => i.FoodCategoryId == id).Include(i => i.Foods).First();
            }
        }

        public FoodCategory GetFoodsTheCategory(string categoryName)
        {
            using (var context = new OtelContext())
            {
                return context.FoodCategories.Where(i => i.Url == categoryName).Include(i => i.Foods).First();
            }
        }

        public List<FoodCategory> getYemekMenu()
        {
            using (var context = new OtelContext())
            {
                var a = new List<FoodCategory>();
                var menu = context.FoodCategories.ToList();
                for (int i = 0; i < menu.Count; i++)
                {
                    if (menu[i].Url == "sicak-icecek" || menu[i].Url == "soguk-icecek")
                    {
                        //menu.Remove(menu[i]);
                    }
                    else if (menu[i].Url == "tatli")
                    {
                        //menu.Remove(menu[i]);
                    }
                    else
                    {
                        a.Add(menu[i]);
                    }
                    

                }
                return a;

            }
        }
    }
}
