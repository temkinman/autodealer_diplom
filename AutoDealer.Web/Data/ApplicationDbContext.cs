using AutoDealer.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoDealer.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Customer> Customers { get; set; }
        //public DbSet<Role> Roles { get; set; }
        public DbSet<SaleReport> SalesReports { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Transmission> Transmissions { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Engine> Engines { get; set; }
        public DbSet<EngineType> EngineTypes { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<CarOwner> CarOwners { get; set; }
        public DbSet<AdvSettings> AdvSettings { get; set; }
    }
}
