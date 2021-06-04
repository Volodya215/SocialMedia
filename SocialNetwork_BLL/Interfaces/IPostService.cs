using SocialNetwork_BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork_BLL.Interfaces
{
    public interface IPostService : ICrud<PostModel>
    {
        IEnumerable<PostModel> GetAllPostsByUserName(string userName);
    }
}
