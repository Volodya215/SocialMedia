using SocialNetwork_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork_DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        // Finding object
        IQueryable<TEntity> FindAll();

        Task<TEntity> GetByIdAsync(int id);

        IEnumerable<TEntity> GetOrderedBy(Expression<Func<TEntity, bool>> condition);

        // Adding object
        Task AddAsync(TEntity entity);

        // Update
        Task Update(TEntity entity);

        // Removing object
        void Delete(TEntity entity);

        Task DeleteByIdAsync(int id);
    }
}
