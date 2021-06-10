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
        /// <summary>
        /// Finds all data from database
        /// </summary>
        /// <returns>Returns a query string with an expression tree</returns>
        IQueryable<TEntity> FindAll();

        /// <summary>
        /// Finds an item in the database by its ID 
        /// </summary>
        /// <param name="id">Entity id</param>
        /// <returns>A task that represents the asynchronous entity data</returns>
        Task<TEntity> GetByIdAsync(int id);

        /// <summary>
        /// Finds the data sorted by the given restriction in the database 
        /// </summary>
        /// <param name="condition">Condition for data selection </param>
        /// <returns>Returns a list of data </returns>
        IEnumerable<TEntity> GetOrderedBy(Expression<Func<TEntity, bool>> condition);

        /// <summary>
        /// Asynchronously adds an element to the database
        /// </summary>
        /// <param name="entity">An element that we add to the database </param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task AddAsync(TEntity entity);

        /// <summary>
        /// Updates an item in the database 
        /// </summary>
        /// <param name="entity">An item that we update in the database </param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task Update(TEntity entity);

        /// <summary>
        /// Delete the item from the database 
        /// </summary>
        /// <param name="entity">Removable item </param>
        void Delete(TEntity entity);

        /// <summary>
        /// Asynchronously removes an item from the database on its ID 
        /// </summary>
        /// <param name="id">Entity id</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task DeleteByIdAsync(int id);
    }
}
