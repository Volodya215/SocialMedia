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
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DbContext Context;

        protected readonly DbSet<TEntity> _entities;
        public Repository(DbContext context)
        {
            Context = context;
            _entities = Context.Set<TEntity>();
        }

        public Task AddAsync(TEntity entity)
        {
            return _entities.AddAsync(entity).AsTask();
        }

        public void Delete(TEntity entity)
        {
            _entities.Remove(entity);
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

        public void Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;

            Context.SaveChanges();
        }
    }
}
