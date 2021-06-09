using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork_BLL.Interfaces
{
    public interface ICrud<TModel> where TModel : class
    {
        IEnumerable<TModel> GetAll();

        Task<TModel> GetByIdWithDetailsAsync(int id);

        /// <summary>
        /// Add element to database async
        /// </summary>
        /// <param name="model">Element</param>
        /// <returns></returns>
        Task AddAsync(TModel model);

        Task UpdateAsync(TModel model);

        Task DeleteByIdAsync(int modelId);
    }
}
