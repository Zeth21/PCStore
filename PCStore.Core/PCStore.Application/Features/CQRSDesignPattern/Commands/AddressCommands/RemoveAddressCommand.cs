using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.AddressCommands
{
    public class RemoveAddressCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
    }
}
