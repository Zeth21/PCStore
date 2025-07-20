using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Services.OrderService.Commands
{
    public class ServiceGetOrderDetailsByOrderIdCommand
    {
        public required string UserId { get; set; }
        public int OrderId { get; set; }
    }
}
