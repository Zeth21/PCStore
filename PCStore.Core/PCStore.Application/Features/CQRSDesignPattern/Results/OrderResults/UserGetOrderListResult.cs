using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Results.OrderResults
{
    public class UserGetOrderListResult
    {
        public int OrderId { get; set; }
        public decimal OrderTotalCost { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? OrderDeliverDate { get; set; }
        public bool OrderIsActive { get; set; }
        public required string AddressName { get; set; }
        public List<string> ProductMainPhotoUrls { get; set; } = [];
        public DateTime StatusDate { get; set; }
        public string StatusName { get; set; } = null!;
    }
}
