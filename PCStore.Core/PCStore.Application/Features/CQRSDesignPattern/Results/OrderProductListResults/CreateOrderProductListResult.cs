using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Results.OrderProductListResults
{
    public class CreateOrderProductListResult
    {
        public int ListId { get; set; }
        public int ProductId { get; set; }
        public string? ProductMainPhotoPath { get; set; }
        public string? BrandName { get; set; }
        public string? CategoryName { get; set; }
        public int ProductTotalRate { get; set; }
        public decimal ProductRateScore { get; set; }
        public byte ProductQuantity { get; set; }
        public decimal ProductTotalCost { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal? ProductOldPrice { get; set; }
        public decimal? ProductOldTotalCost { get; set; }
        public decimal? DiscountRate { get; set; }
        public bool? IsDiscountPercentage { get; set; }
    }
}
