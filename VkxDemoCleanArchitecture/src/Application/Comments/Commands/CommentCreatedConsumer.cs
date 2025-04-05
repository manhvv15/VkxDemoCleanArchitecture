using MassTransit;
using Microsoft.Extensions.Logging;

namespace VkxDemoCleanArchitecture.Application.Comments.Commands;
public class CommentCreatedConsumer : IConsumer<CommentCreatedMessage>
{
    private readonly ILogger<CommentCreatedConsumer> _logger;

    public CommentCreatedConsumer(ILogger<CommentCreatedConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<CommentCreatedMessage> context)
    {
        var message = context.Message;
        _logger.LogInformation("[Consumer] Comment Created: {Id}, {Title}", message.Id, message.Title);
        return Task.CompletedTask;
    }
}
