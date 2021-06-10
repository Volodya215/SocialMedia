﻿using SocialNetwork_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork_DAL.Interfaces
{
    public interface IChatRepository : IRepository<Chat>
    {
        /// <summary>
        /// Finds all data about chats with detailed information about them
        /// </summary>
        /// <returns>Returns a query string with an expression tree </returns>
        IQueryable<Chat> FindAllWithDetails();

        /// <summary>
        /// Finds chat with detailed information about him by id
        /// </summary>
        /// <param name="id">Chat id</param>
        /// <returns>A task that represents the asynchronous chat data</returns>
        Task<Chat> GetByIdWithDetailsAsync(int id);
    }
}
