using System;
using System.Threading.Tasks;
using Aws.Demo.Api.Business.Abstractions;
using Aws.Demo.Api.Model.Forms;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Aws.Demo.Api.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class PdfController : ControllerBase
    {
        private readonly IPdfService _pdfService;

        public PdfController(IPdfService pdfService)
        {
            _pdfService = pdfService;
        }
        
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetAll(Guid id)
        {
            var formsPdfs = await _pdfService.ListAsync(id);

            return Ok(formsPdfs);
        }
        
        [HttpGet("{id:Guid}/version/{version:int}")]
        public async Task<IActionResult> GetById(Guid id, int version)
        {
            var formsPdf = await _pdfService.GetByIdAsync(id, version);

            return Ok(formsPdf);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ApiAddFormsPdf model)
        {
            var pdfModel = await _pdfService.AddAsync(model);

            return CreatedAtAction(nameof(GetById), new {id = pdfModel.Id, version = pdfModel.Version}, null);
        }
        
        [HttpPatch("{id:Guid}/version/{version:int}")]
        public async Task<IActionResult> Update([FromBody] JsonPatchDocument<ApiFormsPdf> model, Guid id, int version)
        {
            await _pdfService.UpdateAsync(model, id, version);

            return Ok();
        }
        
        [HttpDelete("{id:Guid}/version/{version:int}")]
        public async Task<IActionResult> Delete(Guid id, int version)
        {
            await _pdfService.DeleteAsync(id, version);

            return Ok();
        }
    }
}