using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.UserResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Queries.UserQueries
{
    public class GetUserProfileQuery : IRequest<TaskResult<GetUserProfileResult>>
    {
        [JsonIgnore]
        public string? UserId { get; set; } = null!;
    }
}
