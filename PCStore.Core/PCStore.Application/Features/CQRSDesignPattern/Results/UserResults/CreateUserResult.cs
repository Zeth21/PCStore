using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Results.UserResults
{
    public class CreateUserResult
    {
        public required string UserId { get; set; }
        public required string Token { get; set; }
    }
}
