using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aws.Demo.Api.Model.Forms;
using Microsoft.AspNetCore.JsonPatch;

namespace Aws.Demo.Api.Business.Abstractions
{
    public interface IPdfService
    {
        Task<List<ApiFormsPdf>> ListAsync(Guid id);

        Task<ApiFormsPdf> GetByIdAsync(Guid id, int version);

        Task<ApiFormsPdf> AddAsync(ApiAddFormsPdf apiAddFormsPdf);

        Task UpdateAsync(JsonPatchDocument<ApiFormsPdf> patchDocument, Guid id, int version);

        Task DeleteAsync(Guid id, int version);
    }
}