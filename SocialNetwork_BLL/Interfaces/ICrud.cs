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

        Task AddAsync(TModel model);

        Task UpdateAsync(TModel model);

        Task DeleteByIdAsync(int modelId);
    }
}
