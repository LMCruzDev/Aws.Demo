using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aws.Demo.Api.Business.Abstractions;
using Aws.Demo.Api.Business.Mappers;
using Aws.Demo.Api.Controllers.Models.Products;
using Aws.Demo.Api.Data.Abstraction;
using Aws.Demo.Api.Data.Model;
using Microsoft.AspNetCore.JsonPatch;

namespace Aws.Demo.Api.Business
{
    public class ProductService : IProductService
    {
        private readonly IRepository<DataProduct, Guid, Guid> _productsRepository;

        public ProductService(IRepository<DataProduct,Guid,Guid> productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public async Task<List<ApiProduct>> ListAsync(Guid brandGuid)
        {
            var products = await _productsRepository.ListAsync(brandGuid);

            return products?
                .Select(p => p
                    .ToBusinessProduct()
                    .ToApiProduct())
                .ToList();
        }

        public async Task<ApiProduct> GetByIdAsync(Guid brandGuid, Guid productGuid)
        {
            var product = await _productsRepository.GetByIdAsync(brandGuid, productGuid);

            return product
                .ToBusinessProduct()
                .ToApiProduct();
        }

        public async Task<ApiProduct> AddAsync(ApiAddProduct model)
        {
            var dataProduct = model.ToDataProduct();
            await _productsRepository.SaveAsync(dataProduct);

            return dataProduct
                .ToBusinessProduct()
                .ToApiProduct();
        }

        public async Task UpdateAsync(JsonPatchDocument<ApiProduct> patchDocument, Guid brandGuid, Guid productGuid)
        {
            var product = await GetByIdAsync(brandGuid, productGuid);
            patchDocument.ApplyTo(product);

            var dataProduct = product.ToDataProduct();
            await _productsRepository.SaveAsync(dataProduct);
        }

        public async Task DeleteAsync(Guid branchGuid, Guid productGuid)
        {
            await GetByIdAsync(branchGuid, productGuid);

            await _productsRepository.DeleteAsync(branchGuid, productGuid);
        }
    }
}