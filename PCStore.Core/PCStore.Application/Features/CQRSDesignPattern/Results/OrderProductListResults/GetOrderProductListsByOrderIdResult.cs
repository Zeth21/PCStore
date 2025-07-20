using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Results.OrderProductListResults
{
    public class GetOrderProductListsByOrderIdResult
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public required string BrandName { get; set; }
        public required string CategoryName { get; set; }
        public int ItemCount { get; set; }
        public decimal ProductPrice { get; set; }
        public required string ProductMainPhotoPath { get; set; }
        public required decimal ProductRateScore { get; set; }
        public int ProductTotalRate { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal? OldTotalPrice { get; set; }
        public decimal? OldPrice { get; set; }
    }
}
