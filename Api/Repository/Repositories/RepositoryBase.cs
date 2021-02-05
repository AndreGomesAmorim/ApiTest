using Api.Data;
using Api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repository.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>, IDisposable where TEntity : class
    {
        private readonly DbContextOptions<DataContext> _dbContextOptions;

        public RepositoryBase()
        {
            _dbContextOptions = new DbContextOptions<DataContext>();
        }

        public virtual async Task AddAsync(TEntity obj)
        {
            using (var db = new DataContext(_dbContextOptions))
            {
                db.Set<TEntity>().Add(obj);
                await db.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
            GC.Collect();
            GC.SuppressFinalize(this);
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            using (var db = new DataContext(_dbContextOptions))
            {
                return await db.Set<TEntity>().AsNoTracking().ToListAsync();
            }
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            using (var db = new DataContext(_dbContextOptions))
            {
                return await db.Set<TEntity>().FindAsync(id);
            }
        }

        public virtual async Task RemoveAsync(TEntity obj)
        {
            using (var db = new DataContext(_dbContextOptions))
            {
                db.Set<TEntity>().Remove(obj);
                await db.SaveChangesAsync();
            }
        }

        public virtual async Task UpdateAsync(TEntity obj)
        {
            using (var db = new DataContext(_dbContextOptions))
            {
                db.Entry(obj).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }
    }
}
