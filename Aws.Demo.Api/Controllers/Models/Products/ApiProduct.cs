using System;
using System.Collections.Generic;

namespace Aws.Demo.Api.Controllers.Models.Products
{
    public class ApiProduct
    {
        public Guid Guid { get; set; }

        public Guid BrandGuid { get; set; }

        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public int Type { get; set; }

        public decimal Price { get; set; }
        
        public decimal Discount { get; set; }

        public Guid ThumbnailGuid { get; set; }

        public List<Guid> Images { get; set; }
    }
}