using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICartDal : IRepository<Cart>
    {
        Cart GetByUserId(string userid);
        void DeleteFromCart(int cartid, int foodid);
    }
}
