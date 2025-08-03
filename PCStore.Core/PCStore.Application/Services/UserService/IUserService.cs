using PCStore.Application.Features.CQRSDesignPattern.Commands.UserCommands;
using PCStore.Application.Features.CQRSDesignPattern.Queries.UserQueries;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.UserResults;
using PCStore.Application.Services.UserService.ServiceDTO;

namespace PCStore.Application.Services.UserService
{
    public interface IUserService
    {
        public Task<TaskResult<UserLoginResult>> UserLogin(UserLoginQuery request, CancellationToken cancellation);
        public Task<TaskResult<CreateUserResult>> CreateUser(CreateUser request, CancellationToken cancellation);
        public Task<TaskResult<UpdateUserResult>> UpdateUser(UpdateUserCommand request, CancellationToken cancellation);
        public Task<Result> ConfirmEmail(ConfirmEmail request, CancellationToken cancellation);
        public Task<Result> SendPasswordResetTokenToMail(PasswordResetEmail request, CancellationToken cancellation);
        public Task<Result> ResetPassword(UpdatePasswordCommand request, CancellationToken cancellation);
        public Task<TaskResult<ServiceGetUserProfileResult>> GetUserProfile(GetUserProfileQuery request, CancellationToken cancellation);
    }
}
