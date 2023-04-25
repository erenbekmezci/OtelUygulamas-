using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICartService
    {
        void InitializeCart(string userId);

        Cart GetCartByUserId(string userId);

        void AddToCart(string userid, int foodid, int quantity);

        void DeleteFromCart(string userid, int foodid);

        void ClearCart(int cartId);
    }
}
