﻿using Bogus;
using Companies.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Companies.Api.Data
{
    public static class SeedData
    {
        public static async Task SeedDataAsync(this IApplicationBuilder builder)
        {
            using (var scope = builder.ApplicationServices.CreateScope()) 
            {
                var serviceProvider = scope.ServiceProvider;
                var db = serviceProvider.GetRequiredService<CompaniesContext>();
                    
                 await db.Database.MigrateAsync();
                if (await db.Company.AnyAsync()) return;

                try
                {
                    var companies = GenerateCompanies(4);
                    db.AddRangeAsync(companies);
                    await db.SaveChangesAsync();
                }
                catch (Exception ex)
                {

                    throw;
                }

            }
        }

        private static IEnumerable<Company> GenerateCompanies(int nrOfCompanies)
        {
            var faker = new Faker<Company>("sv").Rules((f, c) =>
            {
                c.Name = f.Company.CompanyName();
                c.Address = $"{f.Address.StreetAddress()}, {f.Address.City}";
                c.Country = f.Address.Country();
                c.Employees = GenerateEmployees(f.Random.Int(min: 2, max: 10));
            });
            return faker.Generate(nrOfCompanies);
            
        }

        private static ICollection<Employee> GenerateEmployees(int nrOfEmployees)
        {
            string[] positions = ["Developer", "Tester", "Manager"];
            var faker = new Faker<Employee>("sv").Rules((f, e) =>
            {
                e.Name = f.Person.FullName;
                e.Age = f.Random.Int(min: 18, max: 70);
                e.Position = positions[f.Random.Int(0, positions.Length - 1)];
            });

                return faker.Generate(nrOfEmployees);
        }
    }
}
