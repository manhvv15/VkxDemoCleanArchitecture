using Microsoft.AspNetCore.Mvc;
using VkxDemoCleanArchitecture.Application.Users.Commands.Create;

namespace VkxDemoCleanArchitecture.Web.Controllers;
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] CreateLoginCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateRegisterCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
