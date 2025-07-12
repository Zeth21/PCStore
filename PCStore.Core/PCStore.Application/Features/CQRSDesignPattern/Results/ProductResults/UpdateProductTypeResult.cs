using PCStore.Application.Features.CQRSDesignPattern.Results.ProductAttributeResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Results.ProductResults
{
    public class UpdateProductTypeResult
    {
        public int TypeId { get; set; }
        public required string TypeName { get; set; }
        public required List<CreateProductAttributesResult> NewAttributes { get; set; }
    }
}
