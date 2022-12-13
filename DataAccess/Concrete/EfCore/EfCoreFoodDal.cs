using DataAccess.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EfCore
{
    public class EfCoreFoodDal : EfCoreGenericRepository<Food, OtelContext>, IFoodDal
    {
        public List<Food> GetPopulerFoods()
        {
            using(var context = new OtelContext())
            {
                return context.Foods.ToList();
            }
        }
    }
}
