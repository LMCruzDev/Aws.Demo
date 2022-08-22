using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aws.Demo.Api.Controllers.Models.Products
{
    public class ApiAddProduct
    {
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
