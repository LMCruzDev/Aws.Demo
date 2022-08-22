using System.Threading.Tasks;
using Aws.Demo.Api.Controllers.Models.ConsentDocuments;

namespace Aws.Demo.Api.Business.Abstractions
{
    public interface IFormConsentDocumentService
    {
        Task AddConsentDocument(ApiUploadConsentDocumentRequest request);
    }
}
