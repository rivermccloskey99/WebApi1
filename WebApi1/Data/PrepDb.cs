using WebApi1.Models;

namespace WebApi1.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using(var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext context)
        {
            if(!context.Customers.Any())
            {
                Console.WriteLine("---- Adding data...");

                context.Customers.AddRange(
                    new Customer() {FirstName = "River", LastName = "McCloskey", AccountNumber = 123, SortCode = "456-789", PostCode = "BT74 637", Balance = 6700000.43M},
                    new Customer() {FirstName = "Rory", LastName = "McGrath", AccountNumber = 234, SortCode = "654-789", HouseNumber = "8080", StreetName = "Large Street", TownCity = "Belfast", PostCode = "BT62 2HS", Balance = 5700000.43M}
                );

                Console.WriteLine("---- Data added!");

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--- DB already has data!");
            }
        }
    }
}