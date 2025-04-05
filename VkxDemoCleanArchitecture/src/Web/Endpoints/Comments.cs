//using VkxDemoCleanArchitecture.Application.Comments.Commands;

//namespace VkxDemoCleanArchitecture.Web.Endpoints;

//public class Comments : EndpointGroupBase
//{
//    public override void Map(WebApplication app)
//    {
//        app.MapGroup(this)
//            .RequireAuthorization()
//            .MapPost(CreateComment);
//    }

//    private static async Task<IResult> CreateComment(CreateCommentCommand command, ISender sender)
//    {
//        await sender.Send(command);
//        return Results.Ok();
//    }
//}
