using PCStore.Application.Features.CQRSDesignPattern.Commands.UserCommands;
using System.Text.Json.Serialization;

namespace PCStore.Application.Services.UserService.ServiceDTO
{
    public class CreateUser
    {
        [JsonIgnore]
        public string Url = null!;
        public required CreateUserCommand User { get; set; }
    }
}
