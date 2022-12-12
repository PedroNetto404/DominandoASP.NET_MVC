using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;

namespace ASPNET_MVC5_DEV_IO.Application.Extensions;

public class CustomAuthorization
{
    public static bool ValidarClaimsUsuario(HttpContext context, string claimName, string claimValue)
    {
        return context.User.Identity.IsAuthenticated &&
               context.User.Claims.Any(c => c.Type == claimName && c.Value.Contains(claimValue));
    }
}

public class ClaimsAuthorizeAttribute : AuthorizeAttribute
{
    private readonly string _claimName;
    private readonly string _claimValue;
    public ClaimsAuthorizeAttribute(string claimName, string claimValue)
    {
        _claimName = claimName;
        _claimValue = claimValue;
    }
    protected override bool AuthorizeCore(HttpContext httpContext)
    {
        return CustomAuthorization.ValidarClaimsUsuario(httpContext, _claimName, _claimValue);
    }
    protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
    {
        if(filterContext.HttpContext.Request.IsAuthenticated)
        {
            filterContext.Result = new HttpStatusCodeResult(403);
        }
        else
        {
            base.HandleUnauthorizedRequest(filterContext);
        }
    }
}