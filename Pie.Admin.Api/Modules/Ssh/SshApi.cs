using Microsoft.AspNetCore.Http.HttpResults;

namespace Pie.Admin.Api.Modules.Ssh
{
    static class SshApi
    {
        internal static RouteGroupBuilder MapSshApi(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/ssh").WithTags("SSH");

            group.MapPost("/", Results<Ok<string>, BadRequest<string>> (ISshService sshService, SshConfig sshConfig, string command) =>
            {
                var response = sshService.SendCommand(sshConfig, command);
                return string.IsNullOrEmpty(response) ? TypedResults.BadRequest("Что то пошло не так") : TypedResults.Ok(response);
            });

            return group;
        }
    }
}
