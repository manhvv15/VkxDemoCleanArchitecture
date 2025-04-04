using VkxDemoCleanArchitecture.Infrastructure.Data;
using VkxDemoCleanArchitecture.Web;
using VkxDemoCleanArchitecture.Web.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureServices(builder.Configuration, builder.Environment);
builder.Services.AddKeyVaultIfConfigured(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}
else
{
    app.UseHsts();
}

app.UseHealthChecks("/health");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseMiddleware<ErrorExceptionMiddleware>();

app.UseRouting();
app.UseAuthorization();

app.UseSwaggerUi(settings =>
{
    settings.Path = "/api";
    settings.DocumentPath = "/api/specification.json";
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapRazorPages();
app.MapFallbackToFile("index.html");
app.Map("/", () => Results.Redirect("/api"));
app.MapEndpoints();

app.Run();

public partial class Program { }
