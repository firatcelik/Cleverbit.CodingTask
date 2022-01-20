using Cleverbit.CodingTask.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cleverbit.CodingTask.Data.Repository
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly CodingTaskContext dbContext;
        private readonly DbSet<TEntity> DbSet;
        public GenericRepository(CodingTaskContext _dbContext)
        {
            dbContext = _dbContext;
            DbSet = _dbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll()
        {
            try
            {
                return DbSet;
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }
        public async Task<TEntity> GetById(int id)
        {
            return await GetAll().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().Where(predicate).ToListAsync();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                await dbContext.AddAsync(entity);
                await dbContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be saved: {ex.Message}");
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                dbContext.Update(entity);
                await dbContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be updated: {ex.Message}");
            }
        }
    }
}
