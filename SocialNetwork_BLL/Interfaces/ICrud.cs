using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork_BLL.Interfaces
{
    public interface ICrud<TModel> where TModel : class
    {
        /// <summary>
        /// Get all elements from database
        /// </summary>
        /// <returns>List of model</returns>
        IEnumerable<TModel> GetAll();

        /// <summary>
        /// Searches asynchronously for an element on a given id 
        /// </summary>
        /// <param name="id">Element id</param>
        /// <returns>Returns a model with detailed information on it </returns>
        Task<TModel> GetByIdWithDetailsAsync(int id);

        /// <summary>
        /// Asynchronously adds an element to the database
        /// </summary>
        /// <param name="model">Element</param>
        /// <returns>Task</returns>
        Task AddAsync(TModel model);

        /// <summary>
        /// Asynchronously updates an element in the database
        /// </summary>
        /// <param name="model">Element</param>
        /// <returns>Task</returns>
        Task UpdateAsync(TModel model);

        /// <summary>
        /// Deletes an item in the database for a given ID
        /// </summary>
        /// <param name="modelId">Elements id</param>
        /// <returns>Task</returns>
        Task DeleteByIdAsync(int modelId);
    }
}
