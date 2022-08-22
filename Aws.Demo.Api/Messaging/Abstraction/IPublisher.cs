using System.Threading.Tasks;

namespace Aws.Demo.Api.Messaging.Abstraction
{
    public interface IPublisher<TModel>
    {
        Task Publish(TModel model);
    }
}
