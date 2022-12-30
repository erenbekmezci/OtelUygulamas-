using DataAccess.Abstract;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EfCore
{
    public class EfCoreCartDal : EfCoreGenericRepository<Cart , OtelContext> , ICartDal
    {
        public void DeleteFromCart(int cartid, int foodid)
        {
            using (var db = new OtelContext())
            {
                string cmd = "delete  from \"CartItems\" where \"FoodId\"  = @p0 and \"CartId\" = @p1";
                db.Database.ExecuteSqlRaw(cmd, foodid, cartid);

            }


        }

        public Cart GetByUserId(string userid)
        {
            using (var db = new OtelContext())
            {
                return db.Carts
                    .Include(i => i.CartItems)
                    .ThenInclude(i => i.Food)
                    .FirstOrDefault(i => i.UserId == userid);
            }
        }

        public override void Update(Cart entity)
        {
            using (var context = new OtelContext())
            {
                context.Carts.Update(entity);
                context.SaveChanges();
            }
        }
    }
}
