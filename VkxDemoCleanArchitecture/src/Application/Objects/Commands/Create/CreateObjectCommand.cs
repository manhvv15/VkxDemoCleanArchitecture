using VkxDemoCleanArchitecture.Application.Common.Interfaces;

namespace VkxDemoCleanArchitecture.Application.Objects.Commands.Create;
public class CreateObjectCommand : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
}

public class CreateObjectCommandHandler : IRequestHandler<CreateObjectCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateObjectCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateObjectCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var entity = new Domain.Entities.Object { Name = request.Name };
        _context.Objects.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
