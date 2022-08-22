using System;
using System.Collections.Generic;
using Amazon.DynamoDBv2.DataModel;

namespace Aws.Demo.Api.Data.Model
{
    [DynamoDBTable("Products")]
    public class DataProduct
    {
        [DynamoDBHashKey]
        public Guid Guid { get; set; }

        [DynamoDBRangeKey]
        public Guid BrandGuid { get; set; }

        [DynamoDBProperty]
        public string Name { get; set; }
        
        [DynamoDBProperty]
        public string Description { get; set; }
        
        [DynamoDBProperty]
        public int Type { get; set; }

        [DynamoDBProperty]
        public decimal Price { get; set; }
        
        [DynamoDBProperty]
        public decimal Discount { get; set; }

        [DynamoDBProperty]
        public Guid ThumbnailGuid { get; set; }

        [DynamoDBProperty]
        public List<Guid> Images { get; set; }
    }
}