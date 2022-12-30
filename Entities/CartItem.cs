using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class CartItem
    {
        public int Id { get; set; }

        public int FoodId { get; set; }
        public Food Food { get; set; }
        public int  Quantity { get; set; }

        public int CartId { get; set; }
        public Cart Cart { get; set; }
    }
}
