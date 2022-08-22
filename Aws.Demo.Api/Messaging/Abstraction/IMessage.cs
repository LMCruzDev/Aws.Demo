namespace Aws.Demo.Api.Messaging.Abstraction
{
    interface IMessage<TKey>
    {
        public TKey Guid { get; set; }
    }
}
