namespace Aws.Demo.Api.Controllers.Models.Emails
{
    public class ApiSendEmailRequest
    {
        public string From { get; init; }
        public string FromName { get; init; }
        public string To { get; init; }
        public string Subject { get; init; }
        public string Body { get; init; }
    }
}