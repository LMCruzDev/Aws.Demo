using System;
using Aws.Demo.Api.Business.Model.Products;
using Aws.Demo.Api.Controllers.Models.Products;
using Aws.Demo.Api.Data.Model;

namespace Aws.Demo.Api.Business.Mappers
{
    public static class ProductMapper
    {

        public static DataProduct ToDataProduct(
            this ApiAddProduct apiAddProduct, 
            Guid? formGuid = null)
        {
            return new DataProduct
            {
                Guid = formGuid ?? Guid.NewGuid(),
                BrandGuid = apiAddProduct.BrandGuid,
                Name = apiAddProduct.Name,
                Description = apiAddProduct.Description,
                Type = apiAddProduct.Type,
                Price = apiAddProduct.Price,
                Discount = apiAddProduct.Discount,
                ThumbnailGuid = apiAddProduct.ThumbnailGuid,
                Images = apiAddProduct.Images
            };
        }

        public static DataProduct ToDataProduct(this ApiProduct apiProduct)
        {
            return new DataProduct
            {
                Guid = apiProduct.Guid,
                BrandGuid = apiProduct.BrandGuid,
                Name = apiProduct.Name,
                Description = apiProduct.Description,
                Type = apiProduct.Type,
                Price = apiProduct.Price,
                Discount = apiProduct.Discount,
                ThumbnailGuid = apiProduct.ThumbnailGuid,
                Images = apiProduct.Images
            };
        }

        public static BusinessProduct ToBusinessProduct(this DataProduct businessProduct)
        {
            return new BusinessProduct
            {
                Guid = businessProduct.Guid,
                BrandGuid = businessProduct.BrandGuid,
                Name = businessProduct.Name,
                Description = businessProduct.Description,
                Type = businessProduct.Type,
                Price = businessProduct.Price,
                Discount = businessProduct.Discount,
                ThumbnailGuid = businessProduct.ThumbnailGuid,
                Images = businessProduct.Images
            };
        }

        public static ApiProduct ToApiProduct(this BusinessProduct businessProduct)
        {
            return new ApiProduct
            {
                Guid = businessProduct.Guid,
                BrandGuid = businessProduct.BrandGuid,
                Name = businessProduct.Name,
                Description = businessProduct.Description,
                Type = businessProduct.Type,
                Price = businessProduct.Price,
                Discount = businessProduct.Discount,
                ThumbnailGuid = businessProduct.ThumbnailGuid,
                Images = businessProduct.Images
            };
        }
    }
}