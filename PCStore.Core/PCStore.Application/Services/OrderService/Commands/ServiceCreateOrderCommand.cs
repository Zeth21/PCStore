using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PCStore.Application.Services.OrderService.Commands
{
    public class ServiceCreateOrderCommand
    {
        [JsonIgnore]
        public string UserId { get; set; } = string.Empty;
        public int? CouponId { get; set; }
        public int AddressId { get; set; }
    }
}

