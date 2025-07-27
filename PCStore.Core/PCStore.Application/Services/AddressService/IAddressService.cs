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
    public interface IAddressService
    {
        Task<TaskResult<CreateAddressResult>> CreateAddress(CreateAddressCommand request, CancellationToken cancellation);
        Task<Result> RemoveAddress(RemoveAddressCommand request, CancellationToken cancellation);
        Task<TaskResult<UpdateAddressResult>> UpdateAddress(UpdateAddressCommand request, CancellationToken cancellation);
        Task<TaskListResult<GetAllAddressesResult>> GetAllAddresses(GetAllAddressesQuery request, CancellationToken cancellation);
    }
}
