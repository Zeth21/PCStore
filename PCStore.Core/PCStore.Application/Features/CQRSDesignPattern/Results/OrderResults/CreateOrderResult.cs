using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Results.OrderResults
{
    public class CreateOrderResult
    {
        public int OrderId { get; set; }
        public decimal OrderTotalCost { get; set; }
        public DateTime OrderDate { get; set; }
        public bool OrderIsActive { get; set; }
        public int OrderAddressId { get; set; }
        public string? OrderUserId { get; set; }
    }
}
