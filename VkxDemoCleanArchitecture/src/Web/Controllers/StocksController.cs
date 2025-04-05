using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VkxDemoCleanArchitecture.Application.Stocks.Command.Create;
using VkxDemoCleanArchitecture.Application.Stocks.Command.Delete;
using VkxDemoCleanArchitecture.Application.Stocks.Command.Update;
using VkxDemoCleanArchitecture.Application.Stocks.Queries.GetById;
using VkxDemoCleanArchitecture.Application.Stocks.Queries.GetsPaging;

namespace VkxDemoCleanArchitecture.Web.Controllers;
//[Authorize]
[Route("api/stocks")]
public class StocksController : ControllerBase
{
    private readonly IMediator _mediator;

    public StocksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("paging")]
    public async Task<IActionResult> GetPaging([FromQuery] GetsPagingStockQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetStockByIdQuery { Id = id });
        return Ok(result);
    }

    [HttpPost]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateStockCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateStockCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteStockCommand { Id = id }, cancellationToken);
        return Ok(result);
    }
}
