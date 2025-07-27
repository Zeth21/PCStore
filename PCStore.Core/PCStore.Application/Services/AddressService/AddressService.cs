using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Commands.AddressCommands;
using PCStore.Application.Features.CQRSDesignPattern.Queries.AddressQueries;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.AddressResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Services.AddressService
{
    public class AddressService(IMediator mediator) : IAddressService
    {
        public async Task<TaskResult<CreateAddressResult>> CreateAddress(CreateAddressCommand request, CancellationToken cancellation)
        {
            var result = await mediator.Send(request, cancellation);
            return result;
        }

        public async Task<TaskListResult<GetAllAddressesResult>> GetAllAddresses(GetAllAddressesQuery request, CancellationToken cancellation)
        {
            var result = await mediator.Send(request, cancellation);
            return result;
        }

        public async Task<Result> RemoveAddress(RemoveAddressCommand request, CancellationToken cancellation)
        {
            var result = await mediator.Send(request, cancellation);
            return result;
        }

        public async Task<TaskResult<UpdateAddressResult>> UpdateAddress(UpdateAddressCommand request, CancellationToken cancellation)
        {
            var result = await mediator.Send(request, cancellation);
            return result;
        }
    }
}
