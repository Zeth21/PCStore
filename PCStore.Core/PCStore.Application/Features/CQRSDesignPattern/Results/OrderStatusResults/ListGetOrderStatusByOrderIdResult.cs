using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Results.OrderStatusResults
{
    public class ListGetOrderStatusByOrderIdResult
    {
        public int StatusId { get; set; }
        public DateTime StatusDate { get; set; }
        public required string StatusName { get; set; }
    }
}
