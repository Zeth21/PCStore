using MediatR;
using Microsoft.AspNetCore.Identity;
using PCStore.Application.Abstractions.Auth;
using PCStore.Application.Features.CQRSDesignPattern.Queries.UserQueries;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.UserResults;
using PCStore.Domain.Entities;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.UserHandlers
{
    public class UserLoginHandler(UserManager<User> userManager, IJwtTokenGenerator jwtTokenGenerator) : IRequestHandler<UserLoginQuery, TaskResult<UserLoginResult>>
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

        public async Task<TaskResult<UserLoginResult>> Handle(UserLoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
                return TaskResult<UserLoginResult>.NotFound(message: "Wrong email or password!");
            var checkPassword = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!checkPassword)
                return TaskResult<UserLoginResult>.NotFound(message: "Wrong email or password!");
            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user, roles);
            return TaskResult<UserLoginResult>.Success(message: "Login process successfull!", data: new UserLoginResult { Token = token });

        }

    }
}
