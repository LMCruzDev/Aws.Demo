using System;
using System.Threading.Tasks;
using Aws.Demo.Api.Business.Abstractions;
using Aws.Demo.Api.Controllers.Models.ConsentDocuments;

namespace Aws.Demo.Api.Business
{
    public class FormConsentDocumentService : IFormConsentDocumentService
    {
        private readonly IAmazonS3Service _amazonS3Service;

        public FormConsentDocumentService(IAmazonS3Service amazonS3Service)
        {
            _amazonS3Service = amazonS3Service;
        }

        public Task AddConsentDocument(ApiUploadConsentDocumentRequest request)
        {
            return _amazonS3Service.AddFileAsync(
                "form-consent-documents",
                $"branch\\{request.BranchGuid}\\form\\{request.FormGuid}\\user\\{request.UserGuid}",
                request.File);
        }
    }
}
