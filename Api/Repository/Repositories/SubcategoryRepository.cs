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
    public class SubcategoryRepository : RepositoryBase<Subcategory>, ISubcategoryRepository
    {
        private readonly DbContextOptions<DataContext> _dbContextOptions;

        public SubcategoryRepository()
        {
            _dbContextOptions = new DbContextOptions<DataContext>();
        }

        public override async Task<List<Subcategory>> GetAllAsync()
        {
            using (var db = new DataContext(_dbContextOptions))
            {
                var list = from s in db.Set<Subcategory>()
                           .Include(s => s.Category)
                           select s;

                return await list.ToListAsync();
            }
        }

        public override async Task<Subcategory> GetByIdAsync(int id)
        {
            using (var db = new DataContext(_dbContextOptions))
            {
                var subcategory = from s in db.Set<Subcategory>()
                           .Include(s => s.Category)
                           where s.IdSubcategory == id
                           select s;

                return await subcategory.FirstOrDefaultAsync();
            }
        }
    }
}
