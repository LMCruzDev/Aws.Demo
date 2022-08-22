using System;

namespace Aws.Demo.Contracts.User
{
    public class UserMessage
    {
        public Guid Id { get; init; }
        public string Message { get; init; }
        public DateTime CreatedDate { get; init; }
    }
}