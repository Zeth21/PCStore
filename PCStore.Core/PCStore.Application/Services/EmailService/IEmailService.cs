using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Services.EmailService.ServiceDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Services.EmailService
{
    public interface IEmailService
    {
        Task SendConfirmEmail(string userId,string url);
        Task<Result> SendResetPasswordTokenEmail(ResetPasswordToken request);
    }
}
