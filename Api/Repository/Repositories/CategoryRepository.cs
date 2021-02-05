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
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        private readonly DbContextOptions<DataContext> _dbContextOptions;

        public CategoryRepository()
        {
            _dbContextOptions = new DbContextOptions<DataContext>();
        }

        public override async Task<List<Category>> GetAllAsync()
        {
            using (var db = new DataContext(_dbContextOptions))
            {
                var list = from c in db.Set<Category>()
                           .Include(c => c.Subcategories)
                           select c;

                return await list.ToListAsync();
            }
        }

        public override async Task<Category> GetByIdAsync(int id)
        {
            using (var db = new DataContext(_dbContextOptions))
            {
                var category = from c in db.Set<Category>()
                           .Include(c => c.Subcategories)
                               where c.IdCategory == id
                               select c;

                return await category.FirstOrDefaultAsync();
            }
        }
    }
}
