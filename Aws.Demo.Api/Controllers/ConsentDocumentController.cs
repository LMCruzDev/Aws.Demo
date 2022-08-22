using System.Threading.Tasks;
using Aws.Demo.Api.Business.Abstractions;
using Aws.Demo.Api.Controllers.Models.ConsentDocuments;
using Microsoft.AspNetCore.Mvc;

namespace Aws.Demo.Api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class ConsentDocumentController : ControllerBase
    {
        private readonly IFormConsentDocumentService _service;

        public ConsentDocumentController(IFormConsentDocumentService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile([FromForm] ApiUploadConsentDocumentRequest request)
        {
            await _service.AddConsentDocument(request);

            return Ok();
        }
    }
}
