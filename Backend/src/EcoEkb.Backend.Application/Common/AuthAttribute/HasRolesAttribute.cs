using EcoEkb.Backend.DataAccess.Domain.Enums;
using EcoEkb.Backend.DataAccess.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EcoEkb.Backend.Application.Common.AuthAttribute;

public class HasRolesAttribute : TypeFilterAttribute
{
    public HasRolesAttribute(params Role[] roles) : base(typeof(HasRoleFilter))
    {
        Arguments = new object[] {roles};
    }
}

public class HasRoleFilter : IAuthorizationFilter
{
    private readonly Role[] _roles;
    private readonly ISecurityService _securityService;

    public HasRoleFilter(ISecurityService securityService, params Role[] roles)
    {
        _securityService = securityService;
        _roles = roles;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = _securityService.GetCurrentUser();
        if (user is null || (!_roles.All(role => user.IsInRole(role.ToString())) 
                             && !user.IsInRole(Role.Root.ToString())))
            context.Result = new ForbidResult();
    }
}
