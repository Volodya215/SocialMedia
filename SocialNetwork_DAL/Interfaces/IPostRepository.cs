using SocialNetwork_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork_DAL.Interfaces
{
    public interface IPostRepository : IRepository<Post>
    {
        /// <summary>
        /// Finds all data about posts with detailed information about them
        /// </summary>
        /// <returns>Returns a query string with an expression tree</returns>
        IQueryable<Post> FindAllWithDetails();

        /// <summary>
        /// Finds post with detailed information about him by id
        /// </summary>
        /// <param name="id">Post id</param>
        /// <returns>A task that represents the asynchronous post data</returns>
        Task<Post> GetByIdWithDetailsAsync(int id);
    }
}
