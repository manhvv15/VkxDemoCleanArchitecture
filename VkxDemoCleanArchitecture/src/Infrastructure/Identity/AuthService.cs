using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using VkxDemoCleanArchitecture.Application.Common.Interfaces;
using VkxDemoCleanArchitecture.Domain.Entities;

namespace VkxDemoCleanArchitecture.Infrastructure.Identity;

public class JwtService : IAuthService
{
    private readonly IConfiguration _config;
    private readonly IApplicationDbContext _context;

    public JwtService(IConfiguration config, IApplicationDbContext context)
    {
        _config = config;
        _context = context;
    }

    public async Task<string> GenerateToken(User user)
    {
        var userWithRoles = await _context.AppUsers
            .Where(u => u.Id == user.Id)
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync();

        var roleName = userWithRoles?.UserRoles.FirstOrDefault()?.Role?.Name ?? "User";

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Username),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Role, roleName)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<User?> ValidateUserAsync(string username, string password)
    {
        var user = await _context.AppUsers.FirstOrDefaultAsync(x => x.Username == username);

        if (user == null) return null;

        var isValid = BCrypt.Net.BCrypt.Verify(password, user.Password);
        return isValid ? user : null;
    }
}
