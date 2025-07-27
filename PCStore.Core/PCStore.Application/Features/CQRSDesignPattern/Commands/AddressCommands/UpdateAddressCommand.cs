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
    public class UpdateAddressCommand : IRequest<TaskResult<UpdateAddressResult>>
    {
        public int Id { get; set; }
        public string? AddressName { get; set; }
        public string? Description { get; set; }

        [JsonIgnore]
        public string? UserId { get; set; } = "";
    }
}
