using System.Security.Claims;
using EcoEkb.Backend.Application.Handlers.Users;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EcoEkb.Backend.Application.Common.Middlewares;

public class UserRolesClaimsEnrichMiddleware
{
    private readonly RequestDelegate _next;

    public UserRolesClaimsEnrichMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IMediator mediator)
    {
        var email = context.User.FindFirstValue(ClaimTypes.Email);
        if (email is null)
        {
            await _next(context);
            return;
        }

        var user = await mediator.Send(new GetUserByEmail(email));
        if (user is null)
        {
            await _next(context);
            return;
        }

        var claims = context.User.Claims.ToList();

        if (user.Roles.Any())
        {
            claims.RemoveAll(x => x.Type == ClaimTypes.Role);
            claims.AddRange(user.Roles
                .Select(role => new Claim(ClaimTypes.Role, role.ToString())));
        }

        var userIdentity = new ClaimsIdentity(claims);
        context.User = new ClaimsPrincipal(userIdentity);

        await _next(context);
    }

}