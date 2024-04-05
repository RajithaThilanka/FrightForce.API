using FrightForce.Domain.Identity;

namespace FrightForce.API.Middleware;

public class UserDetailsResolver
{
    private RequestDelegate Next { get; set; }

    public UserDetailsResolver(RequestDelegate next)
    {
        Next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var username = context.User.Identity.Name;

        var currentUserService = context.RequestServices.GetService(typeof(ICurrentUserService)) as ICurrentUserService;

        if (!string.IsNullOrEmpty(username))
        {
            currentUserService.UserName = username;
        }

        await Next(context);
    }

}