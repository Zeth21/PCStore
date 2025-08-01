using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.AddressResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Queries.AddressQueries
{
    public class GetAllAddressesQuery : IRequest<TaskListResult<GetAllAddressesResult>>
    {
        [JsonIgnore]
        public required string UserId { get; set; }
    }
}
