using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Subcategory> Subcategory { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var json = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var connectionString = json.GetValue<string>("SqliteConnection:ConnectionString");

            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlite(connectionString);

            base.OnConfiguring(optionsBuilder);
        }
    }
}
