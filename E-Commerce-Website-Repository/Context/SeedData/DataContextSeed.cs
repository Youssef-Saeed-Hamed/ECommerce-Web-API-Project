using E_Commerce_Website_Core.Models;
using E_Commerce_Website_Core.Models.Order_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce_Website_Repository.Context.SeedData
{
    public static class DataContextSeed
    {
        public static async Task DataSeedAsync(DataContext context)
        {
            if (!context.Set<ProductBrand>().Any())
            {
                // Read File 
                var BrandsData = await File
                    .ReadAllTextAsync(@"..\E-Commerce-Website-Repository\Context\SeedData\brands.json");

                // Convert Data To Object 

                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);

                // Insert Data Into Database  

                if (Brands is not null )
                {
                    await context.Set<ProductBrand>().AddRangeAsync(Brands);
                    await context.SaveChangesAsync();

                }
            }if (!context.Set<ProductType>().Any())
            {
                // Read File 
                var TypesData = await File
                    .ReadAllTextAsync(@"..\E-Commerce-Website-Repository\Context\SeedData\types.json");

                // Convert Data To Object 

                var Types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);

                // Insert Data Into Database  

                if (Types is not null )
                {
                    await context.Set<ProductType>().AddRangeAsync(Types);
                    await context.SaveChangesAsync();

                }
            }if (!context.Set<Product>().Any())
            {
                // Read File 
                var ProductData = await File
                    .ReadAllTextAsync(@"..\E-Commerce-Website-Repository\Context\SeedData\products.json");

                // Convert Data To Object 

                var products = JsonSerializer.Deserialize<List<Product>>(ProductData);

                // Insert Data Into Database  

                if (products is not null )
                {
                    await context.Set<Product>().AddRangeAsync(products);
                    await context.SaveChangesAsync();

                }
            }
			if (!context.Set<DeliveryMethod>().Any())
			{
				// Read File 
				var deliveryData = await File
					.ReadAllTextAsync(@"..\E-Commerce-Website-Repository\Context\SeedData\delivery.json");

				// Convert Data To Object 

				var data = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);

				// Insert Data Into Database  

				if (data is not null)
				{
					await context.Set<DeliveryMethod>().AddRangeAsync(data);
					await context.SaveChangesAsync();

				}
			}
		}
    }
}
