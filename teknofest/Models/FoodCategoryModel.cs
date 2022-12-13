using Entities;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace teknofest.Models
{
    public class FoodCategoryModel
    {
        public int FoodCategoryId { get; set; }
        [Display(Name = "Name", Prompt = "Enter Category Name")]
        [Required(ErrorMessage = "Name zorunlu bir alan.")]
        public string? Name { get; set; }
        
        [Required(ErrorMessage = "Url zorunlu bir alan.")]


        public string? Url { get; set; }

        [Required(ErrorMessage = "Description zorunlu bir alan.")]
        public string? Description { get; set; }

        

        [Required(ErrorMessage = "imageUrl zorunlu bir alan.")]

        public string? imageUrl { get; set; }

        public List<Food>? foods { get; set; }

       


    }
}
