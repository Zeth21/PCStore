using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Services.UserService.ServiceDTO
{
    public class ConfirmEmail
    {
        public required string Token { get; set; }
        public required string UserId { get; set; }
    }
}
