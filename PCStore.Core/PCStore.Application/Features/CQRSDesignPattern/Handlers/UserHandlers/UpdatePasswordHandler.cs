using MediatR;
using Microsoft.AspNetCore.Identity;
using PCStore.Application.Features.CQRSDesignPattern.Commands.UserCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Domain.Entities;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.UserHandlers
{
    public class UpdatePasswordHandler(UserManager<User> userManager) : IRequestHandler<UpdatePasswordCommand, Result>
    {
        private readonly UserManager<User> _userManager = userManager;
        public async Task<Result> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            var checkUser = await _userManager.FindByIdAsync(request.UserId);
            if (checkUser is null) return Result.NotFound("No accounts found!");
            try
            {
                var result = await _userManager.ResetPasswordAsync(checkUser, request.Token, request.Password);
                if (!result.Succeeded) return Result.Fail("Something went wrong !");
                return Result.Success("Your password has updated! You can login now!");
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
    }
}
