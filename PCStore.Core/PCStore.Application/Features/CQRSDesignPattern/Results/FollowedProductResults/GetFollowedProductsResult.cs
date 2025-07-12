using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Results.FollowedProductResults
{
    public class GetFollowedProductsResult
    {
        public int Id { get; set; }
        public required string ProductName { get; set; }
        public int ProductId { get; set; }
        public required decimal ProductPrice { get; set; }
        public required string ProductPhotoPath { get; set; }
        public required decimal ProductRateScore { get; set; }
        public int ProductTotalRate { get; set; }
        public decimal? OldPrice { get; set; }
        public decimal? DiscountRate { get; set; }
        public bool? IsDiscountPercentage { get; set; }
    }
}
