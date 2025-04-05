using Microsoft.AspNetCore.Mvc;
using VkxDemoCleanArchitecture.Application.Permissions.Commands;
using VkxDemoCleanArchitecture.Application.Permissions.Commands.Delete;
using VkxDemoCleanArchitecture.Application.Permissions.Commands.Update;
using VkxDemoCleanArchitecture.Application.Permissions.Queries.GetById;
using VkxDemoCleanArchitecture.Application.Permissions.Queries.GetsPaging;

namespace VkxDemoCleanArchitecture.Web.Controllers;
[Route("api/permissions")]
public class PermissionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PermissionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("paging")]
    public async Task<IActionResult> GetPaging([FromQuery] GetsPagingPermissionQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetPermissionByIdQuery { Id = id });
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePermissionsCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdatePermissionCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeletePermissionCommand { Id = id }, cancellationToken);
        return Ok(result);
    }
}

