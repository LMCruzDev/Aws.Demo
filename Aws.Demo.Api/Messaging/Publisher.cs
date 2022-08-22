using System;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;
using Aws.Demo.Api.Configuration;
using Aws.Demo.Api.Messaging.Abstraction;
using Aws.Demo.Api.Messaging.Model;
using Newtonsoft.Json;

namespace Aws.Demo.Api.Messaging
{
    public class Publisher : IPublisher<FormPdfMessage>
    {
        private readonly AmazonSQSClient amazonSqsClient;
        private readonly AwsConfiguration awsConfiguration;

        public Publisher(
            AmazonSQSClient amazonSqsClient,
            AwsConfiguration awsConfiguration)
        {
            this.amazonSqsClient = amazonSqsClient;
            this.awsConfiguration = awsConfiguration;
        }

        public async Task Publish(FormPdfMessage model)
        {
            var queueUrl = (await amazonSqsClient
                .GetQueueUrlAsync(awsConfiguration.SqsQueueName)).QueueUrl;

            var messageJson = JsonConvert.SerializeObject(model);

            await amazonSqsClient.SendMessageAsync(new SendMessageRequest
            {
                MessageBody = messageJson,
                QueueUrl = queueUrl
            });
        }
    }
}
