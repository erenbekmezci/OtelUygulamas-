using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public  class FoodCategory
    {
        public int FoodCategoryId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string imageUrl { get; set; }
        public string Description { get; set; }

        public List<Food> Foods { get; set; }
    }
}
