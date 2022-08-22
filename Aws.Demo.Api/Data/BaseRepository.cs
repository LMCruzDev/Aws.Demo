using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Aws.Demo.Api.Data.Abstraction;

namespace Aws.Demo.Api.Data
{
    public class BaseRepository<TModel, THashKey, TRangeKey> : IRepository<TModel, THashKey, TRangeKey>
    where TModel : class
    {
        private readonly DynamoDBContext _dbContext;

        protected BaseRepository(AmazonDynamoDBClient dbClient)
        {
            _dbContext = new DynamoDBContext(dbClient);
        }

        public Task<List<TModel>> ListAsync(THashKey hashKey)
        {
            return _dbContext.QueryAsync<TModel>(hashKey).GetRemainingAsync();
        }

        public Task<TModel> GetByIdAsync(THashKey hashKey, TRangeKey rangeKey)
        {
            return _dbContext.LoadAsync<TModel>(hashKey, rangeKey);
        }

        public async Task SaveAsync(TModel model)
        {
            await _dbContext.SaveAsync(model);
        }

        public Task DeleteAsync(TRangeKey rangeKey, THashKey hashKey)
        {
            return _dbContext.DeleteAsync<TModel>(hashKey, rangeKey);
        }
    }
}