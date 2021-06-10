using Microsoft.EntityFrameworkCore;
using SocialNetwork_DAL.Entities;
using SocialNetwork_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork_DAL.Repositories
{
    /// <summary>
    /// Generic repository foe work with TEntity data
    /// </summary>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DbContext Context;

        /// <summary>
        /// Save the table for this repository 
        /// </summary>
        protected readonly DbSet<TEntity> _entities;

        /// <summary>
        /// Repository constructor in which transfer a context for work with a database
        /// </summary>
        /// <param name="myDbContext">Context for work with SocialNetwork database</param>
        public Repository(DbContext context)
        {
            Context = context;
            _entities = Context.Set<TEntity>();
        }

        public Task AddAsync(TEntity entity)
        {
            _entities.Add(entity);
            return Context.SaveChangesAsync();
        }

        public void Delete(TEntity entity)
        {
            _entities.Remove(entity);
            Context.SaveChanges();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await _entities.FirstOrDefaultAsync(x => x.Id == id);

            if (entity != null)
            {
                _entities.Remove(entity);
                await Context.SaveChangesAsync();
            }
        }

        public IQueryable<TEntity> FindAll()
        {
            return _entities.Select(x => x).AsQueryable();
        }

        public Task<TEntity> GetByIdAsync(int id)
        {
            return _entities.FirstOrDefaultAsync(x => x.Id == id);
        }

        public IEnumerable<TEntity> GetOrderedBy(Expression<Func<TEntity, bool>> condition)
        {
            return _entities.OrderBy(condition).ToList();
        }

        public Task Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;

            var toUpdate = _entities.FirstOrDefault(x => x.Id == entity.Id);
            if (toUpdate != null)
                toUpdate = entity;

            Context.Update(toUpdate);
            return Context.SaveChangesAsync();
        }
    }
}
