using EFData;
using EFEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationSettings.Services
{
    public static class DbSeeder
    {
        public static void SeedAll(EFDataContext context) 
        {
            SeedCats(context);
        }

        private static void SeedCats(EFDataContext context) 
        {
            if (!context.Cats.Any()) 
            {
                AppCat cat = new AppCat { 
                Name = "Манул",
                Details = "Деталі про манула",
                Birthday = DateTime.Now,
                ImgUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d6/Manoel.jpg/275px-Manoel.jpg",
                };
                context.Cats.Add(cat);
                context.SaveChanges();

                var firstCat = context.Cats.FirstOrDefault();
                AppCatPrice price = new AppCatPrice
                {
                    CatId = firstCat.Id,
                    DateCreate = DateTime.Now,
                    Price = 500
                };
                context.CatPrices.Add(price);

                context.SaveChanges();
            }
        }
    }
}
