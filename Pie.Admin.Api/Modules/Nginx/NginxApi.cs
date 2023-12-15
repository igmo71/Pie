using System.Text.Json;

namespace Pie.Admin.Api.Modules.Nginx
{
    static class NginxApi
    {
        internal static RouteGroupBuilder MapNginxApi(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/nginx").WithTags("Nginx");

            group.MapPost("/", async (NginxClient nginxClient, string nginxBaseAddress, string tenantName, JsonElement reversePort) =>
            {
                var response = await nginxClient.AddTenantAsync(nginxBaseAddress, tenantName, reversePort);
                return Results.Ok(response);
            });

            return group;
        }
    }
}
