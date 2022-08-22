using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.DAL.Entities;
using Talabat.DAL.Entities.Order_Aggergate;

namespace Talabat.DAL
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                //ProductType
                if (!context.productTypes.Any())
                {
                    var typesData = File.ReadAllText("../Talabat.DAL/Data/SeedData/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    foreach (var type in types) 
                        context.productTypes.Add(type);


                    await context.SaveChangesAsync();
                }

                //ProductBrand
                if (!context.ProductBrands.Any())
                {
                    var brandsData = File.ReadAllText("../Talabat.DAL/Data/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    foreach (var brand in brands)
                        context.ProductBrands.Add(brand);


                    await context.SaveChangesAsync();
                }


                //Products
                if (!context.products.Any())
                {
                    var ProductData = File.ReadAllText("../Talabat.DAL/Data/SeedData/products.json");
                    var Products = JsonSerializer.Deserialize<List<Product>>(ProductData);

                    foreach (var product in Products)
                        context.products.Add(product);

                    await context.SaveChangesAsync();

                }


                if (!context.departments.Any())
                {
                    var DepartmentsData = File.ReadAllText("../Talabat.DAL/Data/SeedData/Department.json");
                    var departments = JsonSerializer.Deserialize<List<Department>>(DepartmentsData);

                    foreach (var department in departments)
                        context.departments.Add(department);

                    await context.SaveChangesAsync();

                }

                if (!context.employees.Any())
                {
                    var EmployeesData = File.ReadAllText("../Talabat.DAL/Data/SeedData/Employees.json");
                    var employees = JsonSerializer.Deserialize<List<Employee>>(EmployeesData);

                    foreach (var employee in employees)
                        context.employees.Add(employee);

                    await context.SaveChangesAsync();

                }

                if (!context.deliveryMethods.Any())
                {
                    var DeliveryData = File.ReadAllText("../Talabat.DAL/Data/SeedData/delivery.json");
                    var DeliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryData);

                    foreach (var DeliveryMethod in DeliveryMethods)
                        context.deliveryMethods.Add(DeliveryMethod);

                    await context.SaveChangesAsync();

                }


            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex , ex.Message);
            }




        }

    }
}
