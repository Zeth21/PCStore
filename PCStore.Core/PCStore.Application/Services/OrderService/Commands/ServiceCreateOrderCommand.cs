﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Services.OrderService.Commands
{
    public class ServiceCreateOrderCommand
    {
        public required string UserId { get; set; }
        public int? CouponId { get; set; }
        public int AddressId { get; set; }
    }
}
