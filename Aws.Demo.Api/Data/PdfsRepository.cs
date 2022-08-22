using Amazon.DynamoDBv2;
using Aws.Demo.Api.Data.Model;

namespace Aws.Demo.Api.Data
{
    public class PdfsRepository : BaseRepository<DataFormsPdf,string,string>
    {
        public PdfsRepository(AmazonDynamoDBClient dbClient)
            : base(dbClient)
        {
        }
    }
}