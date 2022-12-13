using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EfCore
{
    public static class SeedDatabase
    {
        public static void Seed()
        {
            var context = new OtelContext();

            if (!context.Database.GetPendingMigrations().Any())
            {
               if(!context.FoodCategories.Any())
                {
                    context.FoodCategories.AddRange(foodCategories);
                }
                if (!context.Foods.Any())
                {
                    context.Foods.AddRange(foods);
                }
                context.SaveChanges();
            }

           
        }

        private static FoodCategory[] foodCategories =
        {
                new FoodCategory(){Name = "Tatlılar" , Url = "tatlılar" , imageUrl = "1.jpg" , Description = ""},
                new FoodCategory(){Name = "Başlangıçlar" , Url = "baslangic" , imageUrl = "2.jpg" , Description = ""},
                new FoodCategory(){Name = "Ana Yemek" , Url = "ana-yemek" , imageUrl = "3.jpg" , Description = ""}
        };
        private static Food[] foods =
        {

                new Food(){Name = "VeziParmağı" ,  Description ="iyi" , imageUrl = "1.jpg" , Price = 50 , isApproved = true, FoodCategory= foodCategories[0], Url = "vezirparmagi" },
                new Food(){Name = "Künefe" ,  Description ="iyi" , imageUrl = "1.jpg" , Price = 50 , isApproved = true, FoodCategory= foodCategories[0], Url = "künefe" },
                new Food(){Name = "Kelle Paça" ,  Description ="iyi" , imageUrl = "2.jpg" , Price = 50 , isApproved = true, FoodCategory= foodCategories[1], Url = "kelle-paca" },
                new Food(){Name = "Mercimek" ,  Description ="iyi" , imageUrl = "2.jpg" , Price = 50 , isApproved = true, FoodCategory= foodCategories[1], Url = "mercimek" },
                new Food(){Name = "Soslu Tavuk" ,  Description ="iyi" , imageUrl = "3.jpg" , Price = 50 , isApproved = true, FoodCategory= foodCategories[2], Url = "tavuk" },
                new Food(){Name = "Pilav" ,  Description ="iyi" , imageUrl = "3.jpg" , Price = 50 , isApproved = true, FoodCategory= foodCategories[2], Url = "pilav" },
                new Food(){Name = "Fasulye" ,  Description ="iyi" , imageUrl = "3.jpg" , Price = 50 , isApproved = true, FoodCategory= foodCategories[2], Url = "fasulye" }

        };





    }
    
    
}


