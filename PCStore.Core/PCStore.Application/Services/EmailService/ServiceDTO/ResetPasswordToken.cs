using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Services.EmailService.ServiceDTO
{
    public class ResetPasswordToken
    {
        public required string UserId { get; set; }
        public required string Email { get; set; }
        public required string Url { get; set; }
        public required string Token { get; set; }
    }
}
