﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OrderItem
    {

        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int FoodId { get; set; }
        public Food Food { get; set; }


        public int Price { get; set; }
        public int Quantity { get; set; }
    }
}
