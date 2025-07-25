﻿using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.OrderStatusResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.OrderStatusCommands
{
    public class CreateOrderStatusCommand : IRequest<TaskResult<CreateOrderStatusResult>>
    {
        public int OrderId { get; set; }
        public int StatusNameId { get; set; }
        public DateTime StatusDate { get; set; } = DateTime.Now;
    }
}
