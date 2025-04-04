//using Microsoft.AspNetCore.Mvc;
//using VkxDemoCleanArchitecture.Application.Comments.Commands;

//namespace VkxDemoCleanArchitecture.Web.Controllers;
//[Route("api/comments")]
//public class CommentsController : ControllerBase
//{
//    private readonly IMediator _mediator;

//    public CommentsController(IMediator mediator)
//    {
//        _mediator = mediator;
//    }

//    [HttpPost]
//    public async Task<IActionResult> Create([FromBody] CreateCommentCommand command)
//    {
//        var result = await _mediator.Send(command);
//        return Ok(result);
//    }
//}

