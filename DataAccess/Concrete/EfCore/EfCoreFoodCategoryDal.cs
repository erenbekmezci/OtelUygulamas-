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
                return context.FoodCategories.Where(i => i.Url == categoryName).Include(i => i.Foods).FirstOrDefault();
            }
        }

        public List<FoodCategory> getYemekMenu()
        {
            using(var context = new OtelContext())
            {
                var menu = context.FoodCategories.ToList();
                for (int i = 0; i < menu.Count; i++)
                {
                    if (menu[i].Name.Contains("içecek") || menu[i].Name.Contains("tatlılar") || menu[i].Name.Contains("Tatlı"))
                    {
                        menu.Remove(menu[i]);
                    }
                  
                }
                return menu;
                    
            }
        }
    }
}
