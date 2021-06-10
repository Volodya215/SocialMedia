using SocialNetwork_BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork_BLL.Interfaces
{
    public interface IPostService : ICrud<PostModel>
    {
        /// <summary>
        /// Finds a list of all posts written by a user with a given ID 
        /// </summary>
        /// <param name="userName">User username</param>
        /// <returns>List of posts model</returns>
        IEnumerable<PostModel> GetAllPostsByUserName(string userName);
    }
}
