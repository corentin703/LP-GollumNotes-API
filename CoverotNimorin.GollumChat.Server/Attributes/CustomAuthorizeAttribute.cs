using CoverotNimorin.GollumChat.Server.Entities;
using CoverotNimorin.GollumChat.Server.Exceptions.Auth;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CoverotNimorin.GollumChat.Server.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        User? user = context.HttpContext.Items["User"] as User;
        if (user == null)
            throw new NotLoggedInException();
    }
}