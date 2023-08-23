using StoreTaskMVC.Models;
using System;
using System.Text.Json;

namespace StoreTaskMVC.Data
{
    public  class StoreDbInitializerExtension
    {
        public static void Seed(StoreDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.Stores.Any())
                {
                    var storeData = File.ReadAllText("../StoreTaskMVC/Data/SeedData/Store.json");
                    var stores = JsonSerializer.Deserialize<List<Store>>(storeData);

                    foreach (var store in stores)
                    {
                        context.Set<Store>().Add(store);

                        context.SaveChanges();
                        Space space = new Space
                        {
                            Name = "Default Sapce",
                            StoreId = store.Id,
                        };
                        context.Spaces.Add(space);
                        context.SaveChanges();

                        //store.Spaces = new List<Space>
                        //{
                        // new Space { Name = "Default Space" }
                        //};
                    }
                    context.SaveChanges();
                }

                
                



            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreDbInitializerExtension>();
                logger.LogError(ex, ex.Message);

            }

        }






    }
    
}
