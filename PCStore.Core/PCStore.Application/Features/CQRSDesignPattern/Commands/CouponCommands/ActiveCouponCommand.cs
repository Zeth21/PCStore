using MediatR;
using Microsoft.Identity.Client;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.CouponCommands
{
    public class ActiveCouponCommand : IRequest<Result>
    {
       public int Id { get; set; }
    }
}
