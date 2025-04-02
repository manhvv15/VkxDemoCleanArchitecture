using VkxDemoCleanArchitecture.Domain.Entities;

namespace VkxDemoCleanArchitecture.Application.Common.Interfaces;
public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }
    DbSet<Stock> Stocks { get; }

    DbSet<Comment> Comments { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
