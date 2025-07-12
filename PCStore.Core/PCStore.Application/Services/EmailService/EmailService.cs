using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using PCStore.Application.Services.EmailService.ServiceDTO;
using PCStore.Domain.Entities;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using PCStore.Application.Features.CQRSDesignPattern.Results;

namespace PCStore.Application.Services.EmailService
{
    public class EmailService(IOptions<SmtpSettings> options,UserManager<User> userManager) : IEmailService
    {
        private readonly SmtpSettings _smtpSettings = options.Value;
        private readonly UserManager<User> _userManager = userManager;
        public async Task SendConfirmEmail(string userId,string url)
        {
            var checkUser = await _userManager.FindByIdAsync(userId);
            if(checkUser is null)
                return;
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(checkUser);
            var encodedToken = HttpUtility.UrlEncode(token);
            url = url.Replace("%7BuserId%7D", userId)
                     .Replace("%7Btoken%7D", encodedToken);
            string subject = "Welcome to PCStore!";
            string body = $"Click the link below to verify your email!  <a href = '{url}'>" + url + "</a>";
            using var client = new SmtpClient(_smtpSettings.Server, _smtpSettings.Port)
            {
                Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
                EnableSsl = true
            };

            var mail = new MailMessage
            {
                From = new MailAddress(_smtpSettings.SenderEmail, _smtpSettings.SenderName),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mail.To.Add(checkUser.Email!);
            await client.SendMailAsync(mail);

        }

        public async Task<Result> SendResetPasswordTokenEmail(ResetPasswordToken request)
        {
            var encodedToken = HttpUtility.UrlEncode(request.Token);
            request.Url = request.Url.Replace("%7BuserId%7D", request.UserId)
                .Replace("%7Btoken%7D", encodedToken);
            string subject = "Reset Password";
            string body = $"Click here to reset your password!   <a href='{request.Url}'>" + request.Url + "</a>";
            using var client = new SmtpClient(_smtpSettings.Server, _smtpSettings.Port)
            {
                Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
                EnableSsl = true
            };

            var mail = new MailMessage
            {
                From = new MailAddress(_smtpSettings.SenderEmail, _smtpSettings.SenderName),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            try 
            {
                mail.To.Add(request.Email);
                await client.SendMailAsync(mail);
                return Result.Success("The link has sent successfully!");
            }
            catch(Exception ex) 
            {
                return Result.Fail(ex.Message);
            }

        }
    }
}
