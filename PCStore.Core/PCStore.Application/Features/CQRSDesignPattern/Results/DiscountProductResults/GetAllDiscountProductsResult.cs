using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Results.DiscountProductResults
{
    public class GetAllDiscountProductsResult
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public required decimal ProductPrice { get; set; }
        public string? ProductMainPhotoPath { get; set; }
        public short ProductStock { get; set; }
        public bool ProductIsAvailable { get; set; }
        public int ProductTotalRate { get; set; }
        public decimal ProductRateScore { get; set; }
        public decimal? DiscountRate { get; set; }
        public decimal? OldPrice { get; set; }
        public bool? IsDiscountPercentage { get; set; }

    }
}
