using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Results.ProductAttributeResults
{
    public class UpdateProductAttributeResult
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Value { get; set; }
        public string? Unit { get; set; }
    }
}
