using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Results.CouponProductsHandler
{
    public class ListCreateCouponProductResult
    {
        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public required decimal ProductPrice { get; set; }
        public string? ProductMainPhotoPath { get; set; }
        public short ProductStock { get; set; }
    }
}
