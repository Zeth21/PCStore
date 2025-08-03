using PCStore.UI.Models.Results.AddressResults;

namespace PCStore.Application.Services.UserService.ServiceDTO
{
    public class ServiceGetUserProfileResult
    {
        public GetUserProfileResult UserProfile { get; set; } = null!;
        public List<GetAllAddressesResult> UserAddresses { get; set; } = [];
    }

    public class GetUserProfileResult
    {
        public string ProfilePhoto { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; } = string.Empty;

    }
}
