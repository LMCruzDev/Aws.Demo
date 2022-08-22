using System;
using Microsoft.AspNetCore.Http;

namespace Aws.Demo.Api.Controllers.Models.ConsentDocuments
{
    public class ApiUploadConsentDocumentRequest
    {
        public IFormFile File { get; set; }

        public string Name { get; set; }

        public Guid FormGuid { get; set; }

        public Guid BranchGuid { get; set; }

        public Guid UserGuid { get; set; }
    }
}
