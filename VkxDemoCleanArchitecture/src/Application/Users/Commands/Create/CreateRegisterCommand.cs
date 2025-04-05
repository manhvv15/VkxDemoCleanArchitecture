using System.Net.Mail;
using VkxDemoCleanArchitecture.Application.Common.Interfaces;
using VkxDemoCleanArchitecture.Domain.Common;
using VkxDemoCleanArchitecture.Domain.Entities;
using VkxDemoCleanArchitecture.Infrastructure.Identity;

namespace VkxDemoCleanArchitecture.Application.Users.Commands.Create;

public class CreateRegisterCommand : IRequest<Unit>
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class CreateRegisterCommandHandler : IRequestHandler<CreateRegisterCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public CreateRegisterCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(CreateRegisterCommand request, CancellationToken cancellationToken)
    {
        await ValidateRequestAsync(request, cancellationToken);

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = request.Username,
            Email = request.Email,
            Password = hashedPassword
        };

        _context.AppUsers.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

    private async Task ValidateRequestAsync(CreateRegisterCommand request, CancellationToken cancellationToken)
    {
        if (await _context.AppUsers.AnyAsync(u => u.Username == request.Username, cancellationToken))
        {
            throw new Exception(ErrorCode.UserIsExisted);
        }

        if (!IsValidEmail(request.Email))
        {
            throw new Exception(ErrorCode.EmailInValid);
        }
    }

    private bool IsValidEmail(string email)
    {
        var trimmedEmail = email.Trim();

        if (trimmedEmail.EndsWith("."))
        {
            return false;
        }
        try
        {
            var addr = new MailAddress(trimmedEmail);
            return addr.Address == trimmedEmail;
        }
        catch
        {
            return false;
        }
    }
}
