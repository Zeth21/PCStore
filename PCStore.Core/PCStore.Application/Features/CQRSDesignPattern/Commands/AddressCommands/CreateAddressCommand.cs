using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.AddressResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Commands.AddressCommands
{
    public class CreateAddressCommand : IRequest<TaskResult<CreateAddressResult>>
    {
        [JsonIgnore]
        public string UserId { get; set; } = "";
        public required string AddressName { get; set; }
        public required string Description { get; set; }
    }
}
