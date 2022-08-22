using System;
using Amazon.DynamoDBv2;
using Aws.Demo.Api.Data.Model;

namespace Aws.Demo.Api.Data
{
    public class ProductsRepository : BaseRepository<DataProduct,Guid,Guid>
    {
        protected ProductsRepository(AmazonDynamoDBClient dbClient)
            : base(dbClient)
        {
        }
    }
}