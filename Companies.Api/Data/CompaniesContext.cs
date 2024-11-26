using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Companies.Api.Entities;

namespace Companies.Api.Data
{
    public class CompaniesContext : DbContext
    {
        public CompaniesContext (DbContextOptions<CompaniesContext> options)
            : base(options)
        {
        }

        public DbSet<Companies.Api.Entities.Company> Company { get; set; } = default!;
    }
}
