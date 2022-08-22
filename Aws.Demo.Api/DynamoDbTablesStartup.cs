using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using Microsoft.Extensions.Hosting;

namespace Aws.Demo.Api
{
    public class DynamoDbTablesStartup : IHostedService
    {
        private readonly AmazonDynamoDBClient amazonDynamoDBClient;

        public DynamoDbTablesStartup(
            AmazonDynamoDBClient amazonDynamoDBClient)
        {
            this.amazonDynamoDBClient = amazonDynamoDBClient;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var createdTables = await this.amazonDynamoDBClient.ListTablesAsync(cancellationToken);

            var tableNames = this.GetType().Assembly.GetTypes()
                .Where(a => a.IsClass && a.CustomAttributes.Any(c => c.AttributeType == typeof(DynamoDBTableAttribute)))
                .Select(a => a.CustomAttributes.ToList()[0].ConstructorArguments[0].Value.ToString())
                .Where(a => !createdTables.TableNames.Contains(a))
                .ToList();

            var tasks = tableNames
                .Select(tableName =>
                {
                    var request = new CreateTableRequest
                    {
                        AttributeDefinitions = new List<AttributeDefinition>
                        {
                            new AttributeDefinition("HashKey",ScalarAttributeType.S),
                            new AttributeDefinition("RangeKey", ScalarAttributeType.S)
                        },
                        KeySchema = new List<KeySchemaElement>
                        {
                            new KeySchemaElement("HashKey",KeyType.HASH),
                            new KeySchemaElement("RangeKey", KeyType.RANGE)
                        },
                        ProvisionedThroughput = new ProvisionedThroughput
                        {
                            ReadCapacityUnits = 5,
                            WriteCapacityUnits = 5
                        },
                        TableName = tableName
                    };

                    return amazonDynamoDBClient.CreateTableAsync(request, cancellationToken);

                })
                .ToArray();

            Task.WaitAll(tasks);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            var tableNames = this.GetType().Assembly.GetTypes()
               .Where(a => a.IsClass && a.CustomAttributes.Any(c => c.AttributeType == typeof(DynamoDBTableAttribute)))
               .Select(a => a.CustomAttributes.ToList()[0].ConstructorArguments[0].Value.ToString())
               .ToList();

            var tasks = tableNames
                .Select(t => amazonDynamoDBClient.DeleteTableAsync(t, cancellationToken))
                .ToArray();

            await Task.WhenAll(tasks);
        }
    }
}
