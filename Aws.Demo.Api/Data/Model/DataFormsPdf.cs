using System;
using Amazon.DynamoDBv2.DataModel;
using Aws.Demo.Api.Data.Abstraction;

namespace Aws.Demo.Api.Data.Model
{
    [DynamoDBTable("FormsPdf")]
    public class DataFormsPdf : ITable<string, string>
    {
        [DynamoDBHashKey]
        public string HashKey { get; set; }

        [DynamoDBRangeKey]
        public string RangeKey { get; set; }

        [DynamoDBProperty]
        public Guid FileId { get; set; }

        [DynamoDBProperty]
        public string Name { get; set; }

        [DynamoDBProperty]
        public DateTime CreatedDate { get; set; }

        [DynamoDBProperty]
        public int Status { get; set; }
    }
}