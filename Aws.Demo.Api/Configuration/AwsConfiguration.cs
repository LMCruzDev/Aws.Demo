namespace Aws.Demo.Api.Configuration
{
    public class AwsConfiguration
    {
        public string AccessKey { get; init; }

        public string SecretKey { get; init; }

        public string SecretToken { get; set; }

        public string SqsServiceUrl { get; init; }

        public string SqsQueueName { get; set; }

        public string SesSmtpHost { get; init; }

        public string SesSmtpUsername { get; init; }

        public string SesSmtpPassword { get; init; }

        public string ElasticSearchUrl { get; init; }
    }
}