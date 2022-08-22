using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aws.Demo.Api.Data.Abstraction
{
    public interface IRepository<TModel, in THashKey, in TRangeKey> 
        where TModel : class
    {
        Task<List<TModel>> ListAsync(THashKey hashKey);

        Task<TModel> GetByIdAsync(THashKey hashKey, TRangeKey rangeKey);

        Task SaveAsync(TModel model);

        Task DeleteAsync(TRangeKey rangeKey, THashKey hashKey);
    }
}