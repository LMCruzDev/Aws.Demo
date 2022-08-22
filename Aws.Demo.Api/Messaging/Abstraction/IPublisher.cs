using System.Threading.Tasks;

namespace Aws.Demo.Api.Messaging.Abstraction
{
    public interface IPublisher<in TModel>
    {
        Task Publish(TModel model);
    }
}
