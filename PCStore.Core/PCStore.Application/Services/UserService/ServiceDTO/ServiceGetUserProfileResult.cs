using PCStore.Application.Features.CQRSDesignPattern.Results.AddressResults;
using PCStore.Application.Features.CQRSDesignPattern.Results.UserResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Services.UserService.ServiceDTO
{
    public class ServiceGetUserProfileResult
    {
        public GetUserProfileResult UserProfile { get; set; } = null!;
        public List<GetAllAddressesResult> UserAddresses { get; set; } = [];
    }
}
