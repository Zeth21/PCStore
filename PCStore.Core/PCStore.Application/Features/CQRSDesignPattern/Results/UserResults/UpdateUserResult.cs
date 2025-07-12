using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Results.UserResults
{
    public class UpdateUserResult
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;

        public string? PhoneNumber { get; set; }
    }
}
