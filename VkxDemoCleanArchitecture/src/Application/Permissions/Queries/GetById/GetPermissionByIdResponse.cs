namespace VkxDemoCleanArchitecture.Application.Permissions.Queries.GetById;
public class GetPermissionByIdResponse
{
    public Guid Id { get; set; }
    public Guid ActionId { get; set; }
    public Guid ObjectId { get; set; }
    public string? Method { get; set; }
    public string? Endpoint { get; set; }
}
