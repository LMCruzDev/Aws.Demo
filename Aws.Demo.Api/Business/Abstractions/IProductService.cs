using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aws.Demo.Api.Controllers.Models.Products;
using Microsoft.AspNetCore.JsonPatch;

namespace Aws.Demo.Api.Business.Abstractions
{
    public interface IProductService
    {
        Task<List<ApiProduct>> ListAsync(Guid brandGuid);

        Task<ApiProduct> GetByIdAsync(Guid brandGuid, Guid productGuid);

        Task<ApiProduct> AddAsync(ApiAddProduct model);

        Task UpdateAsync(JsonPatchDocument<ApiProduct> patchDocument, Guid brandGuid, Guid productGuid);

        Task DeleteAsync(Guid branchGuid, Guid productGuid);
    }
}