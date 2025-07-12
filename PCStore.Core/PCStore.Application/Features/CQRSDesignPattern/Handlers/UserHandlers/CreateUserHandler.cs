using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Identity;
using PCStore.Application.Abstractions.Auth;
using PCStore.Application.Features.CQRSDesignPattern.Commands.UserCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.UserResults;
using PCStore.Application.Features.Helpers;
using PCStore.Domain.Entities;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.UserHandlers
{
    public class CreateUserHandler(UserManager<User> userManager, IJwtTokenGenerator token) : IRequestHandler<CreateUserCommand, TaskResult<CreateUserResult>>
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly IJwtTokenGenerator _token = token;

        public async Task<TaskResult<CreateUserResult>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
                return TaskResult<CreateUserResult>.Fail("This email already has an account!");

            var user = new User
            {
                Name = request.Name,
                Surname = request.Surname,
                Email = request.Email,
                UserName = request.Email,
                ProfilePhoto = "default-profile.png"
            };

            var createResult = await _userManager.CreateAsync(user, request.Password);
            if (!createResult.Succeeded)
            {
                var errors = createResult.Errors.Select(e => e.Description).ToList();
                return TaskResult<CreateUserResult>.Fail(message:"Something went wrong!",errors:errors);
            }

            var role = "Customer";
            await _userManager.AddToRoleAsync(user, role);

            var userToken = _token.GenerateToken(user, new[] { role });
            var loginResult = new CreateUserResult { UserId = user.Id,Token = userToken };
            return TaskResult<CreateUserResult>.Success("Account created successfully!", loginResult);
        }

    }
}
