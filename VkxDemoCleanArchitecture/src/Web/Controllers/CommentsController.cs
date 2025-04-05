using Microsoft.AspNetCore.Mvc;
using VkxDemoCleanArchitecture.Application.Comments.Commands;
using VkxDemoCleanArchitecture.Application.Comments.Queries.GetsPaging;

namespace VkxDemoCleanArchitecture.Web.Controllers;
[Route("api/comments")]
public class CommentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CommentsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("paging")]
    public async Task<IActionResult> GetPaging([FromQuery] GetsPagingCommentQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCommentCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}

