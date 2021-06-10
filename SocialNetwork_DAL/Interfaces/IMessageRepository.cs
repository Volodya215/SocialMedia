using SocialNetwork_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork_DAL.Interfaces
{
    public interface IMessageRepository : IRepository<Message>
    {
        /// <summary>
        /// Finds all data about messages with detailed information about them
        /// </summary>
        /// <returns>Returns a query string with an expression tree </returns>
        IQueryable<Message> FindAllWithDetails();

        /// <summary>
        /// Finds message with detailed information about him by id
        /// </summary>
        /// <param name="id">Message id</param>
        /// <returns>A task that represents the asynchronous message data</returns>
        Task<Message> GetByIdWithDetailsAsync(int id);
    }
}
