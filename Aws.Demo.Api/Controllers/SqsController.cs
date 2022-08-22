using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using Aws.Demo.Api.Configuration;
using Aws.Demo.Api.Model.Messages;
using Aws.Demo.Contracts.User;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Aws.Demo.Api.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class SqsController : ControllerBase
    {
        private readonly AmazonSQSClient _amazonSqsClient;

        public SqsController(AwsConfiguration awsConfiguration)
        {
            _amazonSqsClient = new AmazonSQSClient(
                new BasicAWSCredentials(awsConfiguration.AccessKey, awsConfiguration.SecretKey),
                RegionEndpoint.EUWest2);
        }

        [HttpPost("{queueName}")]
        public async Task<IActionResult> PostMessage(string queueName, [FromBody] ApiUserMessageRequest request)
        {
            var queueUrl = (await _amazonSqsClient
                .GetQueueUrlAsync(queueName)).QueueUrl;

            var message = new UserMessage
            {
                Id = Guid.NewGuid(),
                Message = request.Message,
                CreatedDate = DateTime.UtcNow
            };

            var messageJson = JsonConvert.SerializeObject(message);

            var response = await _amazonSqsClient.SendMessageAsync(new SendMessageRequest
            {
                MessageDeduplicationId = message.Id.ToString(),
                MessageGroupId = request.GroupId,
                MessageBody = messageJson,
                QueueUrl = queueUrl
            });

            if (response.HttpStatusCode != HttpStatusCode.OK) return Ok(response.HttpStatusCode);

            return Ok();
        }

        [HttpGet("{queueName}")]
        public async Task<IActionResult> GetMessages(string queueName)
        {
            var queueUrl = (await _amazonSqsClient
                .GetQueueUrlAsync(queueName)).QueueUrl;

            var response = await _amazonSqsClient.ReceiveMessageAsync(new ReceiveMessageRequest
            {
                QueueUrl = queueUrl,
                AttributeNames = new List<string> {"ApproximateReceiveCount"},
                MessageAttributeNames = new List<string> {"All"}
            });

            if (response.HttpStatusCode != HttpStatusCode.OK)
                throw new AmazonSQSException(
                    $"Failed to GetMessagesAsync for queue {queueName}. Response: {response.HttpStatusCode}");

            return Ok(response.Messages);
        }
    }
}