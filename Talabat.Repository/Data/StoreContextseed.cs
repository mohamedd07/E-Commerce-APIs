using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.OrderAggregate;

namespace Talabat.Repository.Data
{
    public static class StoreContextseed
    {
        public static async Task SeedAsync(StoreContext dbContext)
        {

            #region Brand

            var brandData = File.ReadAllText("../Talabat.Repository/Dataseed/brands.json");
            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);

            if (dbContext.Brands.Count() == 0)
            {

                if (brands?.Count > 0)
                {
                    brands = brands.Select(b => new ProductBrand()
                    {
                        Name = b.Name

                    }).ToList();

                    foreach (var brand in brands)
                    {
                        dbContext.Set<ProductBrand>().Add(brand);
                    }

                    await dbContext.SaveChangesAsync();
                }

            }

            #endregion

            #region Category

            var CategoryData = File.ReadAllText("../Talabat.Repository/Dataseed/categories.json");
            var Categories = JsonSerializer.Deserialize<List<ProductCategory>>(CategoryData);

            if (dbContext.Categories.Count() == 0)
            {

                if (Categories?.Count > 0)
                {
                    Categories = Categories.Select(b => new ProductCategory()
                    {
                        Name = b.Name

                    }).ToList();

                    foreach (var category in Categories)
                    {
                        dbContext.Set<ProductCategory>().Add(category);
                    }

                    await dbContext.SaveChangesAsync();
                }

            }

            #endregion

            #region Product

            var ProductData = File.ReadAllText("../Talabat.Repository/Dataseed/products.json");
            var Products = JsonSerializer.Deserialize<List<Product>>(ProductData);

            if (dbContext.Products.Count() == 0)
            {

                if (Products?.Count > 0)
                {

                    foreach (var product in Products)
                    {
                        dbContext.Set<Product>().Add(product);
                    }

                    await dbContext.SaveChangesAsync();
                }

            }


            #endregion


            #region delivery
            var deliveryData = File.ReadAllText("../Talabat.Repository/Dataseed/delivery.json");
            var delivery = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);

            if (dbContext.DeliveryMethod.Count() == 0)
            {

                if (delivery?.Count > 0)
                {

                    foreach (var item in delivery)
                    {
                        dbContext.Set<DeliveryMethod>().Add(item);
                    }

                    await dbContext.SaveChangesAsync();
                }

            }
            #endregion






        }
    }
}
