using VkxDemoCleanArchitecture.Domain.Entities;

namespace VkxDemoCleanArchitecture.Infrastructure.Identity;
public interface IAuthService
{
    Task<string> GenerateToken(User user);
    Task<User?> ValidateUserAsync(string username, string password);
}
