using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Food
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int Price { get; set; }
        public string Url { get; set; }
        public string imageUrl { get; set; }
        public bool isApproved { get; set; }

        public FoodCategory FoodCategory { get; set; }
        public int FoodCategoryId { get; set; }




    }
}
