using MediatR;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.UserResults;

namespace PCStore.Application.Features.CQRSDesignPattern.Queries.UserQueries
{
    public class UserLoginQuery : IRequest<TaskResult<UserLoginResult>>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
