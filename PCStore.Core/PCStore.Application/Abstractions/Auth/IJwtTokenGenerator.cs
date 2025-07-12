using PCStore.Domain.Entities;

namespace PCStore.Application.Abstractions.Auth
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user, IList<string> roles);
    }
}
