
using Cleverbit.CodingTask.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Cleverbit.CodingTask.Data
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> GetAll();
        Task<TEntity> GetById(int id);
        Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
    }
}
