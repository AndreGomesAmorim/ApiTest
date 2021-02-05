using Api.Data;
using Api.Models;
using Api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repository.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        private readonly DbContextOptions<DataContext> _dbContextOptions;

        public ProductRepository()
        {
            _dbContextOptions = new DbContextOptions<DataContext>();
        }

        public override async Task<List<Product>> GetAllAsync()
        {
            using (var db = new DataContext(_dbContextOptions))
            {
                var list = from p in db.Set<Product>()
                           .Include(p => p.Subcategory.Category)
                           select p;

                return await list.ToListAsync();
            }
        }

        public override async Task<Product> GetByIdAsync(int id)
        {
            using (var db = new DataContext(_dbContextOptions))
            {
                var product = from p in db.Set<Product>()
                           .Include(p => p.Subcategory.Category)
                           where p.IdProduct == id
                           select p;

                return await product.FirstOrDefaultAsync();
            }
        }
    }
}
