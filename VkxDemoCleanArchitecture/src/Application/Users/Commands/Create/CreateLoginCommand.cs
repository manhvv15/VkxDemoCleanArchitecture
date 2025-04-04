using VkxDemoCleanArchitecture.Domain.Common;
using VkxDemoCleanArchitecture.Infrastructure.Identity;

namespace VkxDemoCleanArchitecture.Application.Users.Commands.Create;
public class CreateLoginCommand : IRequest<string>
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class CreateLoginCommandHandler : IRequestHandler<CreateLoginCommand, string>
{
    private readonly IAuthService _authService;

    public CreateLoginCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<string> Handle(CreateLoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _authService.ValidateUserAsync(request.Username, request.Password);

        if (user == null)
        {
            throw new Exception(ErrorCode.UserInValid);
        }

        var token = await _authService.GenerateToken(user);
        return token;
    }
}
