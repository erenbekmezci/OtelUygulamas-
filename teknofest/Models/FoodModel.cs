using Entities;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace teknofest.Models
{
    public class FoodModel
    {
        public int Id { get; set; }
        [Display(Name = "Name", Prompt = "Enter Food Name")]
        [Required(ErrorMessage = "Name zorunlu bir alan.")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Price zorunlu bir alan.")]
        [Range(5, 2000, ErrorMessage = "5 ile 2000 arası bir değer giriniz.")]
        public int Price { get; set; }
        [Required(ErrorMessage = "Url zorunlu bir alan.")]


        public string? Url { get; set; }

        [Required]
        public string? Description { get; set; }

        public bool isApproved { get; set; }
        
        [Required(ErrorMessage = "mageUrl zorunlu bir alan.")]
      
        public string? imageUrl { get; set; }

        public FoodCategory? foodCategory { get; set; }

        [Display(Name = "Yemek Kategorisi")]
        public int FoodCategoryId { get; set; }
    }
}
