using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Identity;
using PCStore.Application.Features.CQRSDesignPattern.Commands.UserCommands;
using PCStore.Application.Features.CQRSDesignPattern.Queries.UserQueries;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.UserResults;
using PCStore.Application.Features.Helpers;
using PCStore.Application.Services.UserService.ServiceDTO;
using PCStore.Domain.Entities;
using System;
using System.Net.Mail;
using System.Net;
using PCStore.Application.Services.EmailService;
using PCStore.Application.Services.EmailService.ServiceDTO;
using System.Threading.Tasks;

namespace PCStore.Application.Services.UserService
{
    public class UserService(IMediator mediator, UserManager<User> userManager,IEmailService emailService) : IUserService
    {
        private readonly IMediator _mediator = mediator;
        private readonly UserManager<User> _userManager = userManager;
        private readonly IEmailService _emailService = emailService;

        public async Task<Result> ConfirmEmail(ConfirmEmail request, CancellationToken cancellation)
        {
            var checkUser = await _userManager.FindByIdAsync(request.UserId);
            if (checkUser is null) return Result.Fail("User not found!");
            var result = await _userManager.ConfirmEmailAsync(checkUser, request.Token);
            if (!result.Succeeded) return Result.Fail("Something went wrong!");
            return Result.Success("Your email is confirmed! You can login now.");
        }

        public async Task<TaskResult<CreateUserResult>> CreateUser(CreateUser request, CancellationToken cancellation)
        {

            var result = await _mediator.Send(request.User, cancellation);
            if (!result.IsSucceeded)
                return result;
            await _emailService.SendConfirmEmail(result.Data!.UserId,request.Url);
            return result;
        }

        public async Task<Result> ResetPassword(UpdatePasswordCommand request, CancellationToken cancellation)
        {
            var result = await _mediator.Send(request,cancellation);
            return result;
        }

        public async Task<Result> SendPasswordResetTokenToMail(PasswordResetEmail request, CancellationToken cancellation)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null) return Result.NotFound("No account has found!");
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var serviceRequest = new ResetPasswordToken 
            {
                Email = user.Email!,
                UserId = user.Id,
                Token = token,
                Url = request.Url
            };
            var result = await _emailService.SendResetPasswordTokenEmail(serviceRequest);
            return result;
        }

        public async Task<TaskResult<UpdateUserResult>> UpdateUser(UpdateUserCommand request, CancellationToken cancellation)
        {
            var result = await _mediator.Send(request,cancellation);
            return result;
        }

        public async Task<TaskResult<UserLoginResult>> UserLogin(UserLoginQuery user, CancellationToken cancellation)
        {
            var result = await _mediator.Send(user, cancellation);
            if (!result.IsSucceeded || result.StatusCode != 200)
                return TaskResult<UserLoginResult>.NotFound("Wrong email or password!");
            return TaskResult<UserLoginResult>.Success("Login is successfull", data: result.Data);
        }

    }
}
