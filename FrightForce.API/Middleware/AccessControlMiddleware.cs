using FrightForce.Infractructure.Persistence;
using FrightForce.Infrastructure.Persistence;
using Microsoft.Extensions.Caching.Memory;

namespace FrightForce.API.Middleware;

public class AccessControlMiddleware
{
    private readonly RequestDelegate _next;
        private readonly IMemoryCache _cache;
        private readonly ILogger<AccessControlMiddleware> _logger;

        public AccessControlMiddleware(RequestDelegate next, IMemoryCache cache, ILogger<AccessControlMiddleware> logger)
        {
            _next = next;
            _cache = cache;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, FrightForceDbContext dbContext)
        {
            //if request is not authenticated, skip this middleware
            if (!context.User.Identity.IsAuthenticated)
            {
                _logger.LogTrace("Request is not authenticated, skipping access grants");
                await _next(context);
                return;
            }

            //
            var username = context.User.Identity.Name;
            var appAccessCacheKey = $"AppAccess-{username}";
            var companyAccessCacheKey = $"CompanyAccess-{username}";

            if (!_cache.TryGetValue(appAccessCacheKey, out bool hasAppAccess))
            {
                //hasAppAccess =
                //    await dbContext.ApplicationUserApplicationAccessControls.AnyAsync(ac =>
                //        ac.Username.ToLower() == username.ToLower());
                //_cache.Set(appAccessCacheKey, hasAppAccess, TimeSpan.FromMinutes(5));
            }

            if (!hasAppAccess)
            {
                _logger.LogTrace("User {UserName} does not have access to the application", username);
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                return;
            }

            // allowedCompanies;
            if (!_cache.TryGetValue(companyAccessCacheKey, out List<int> allowedCompanies))
            {
                //allowedCompanies = await dbContext.ApplicationUserCompanyAccessControls
                //    .Where(u => u.UserName == username.ToLower() && !u.IsDeleted)
                //    .Select(u => u.CompanyId)
                //    .ToListAsync();

                //_cache.Set(companyAccessCacheKey, allowedCompanies, TimeSpan.FromMinutes(5));
            }

            if (allowedCompanies.Count == 0)
            {
                _logger.LogTrace("User {UserName} does not have access to any company. Forbidding Access", username);
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                return;
            }

            if (context.Items.TryGetValue("CompanyId", out var companyIdObj))
            {
                var currentCompanyId = int.Parse(companyIdObj.ToString());
                _logger.LogTrace("Resoved TenantId {TenantId} from request", currentCompanyId);
                if (!allowedCompanies.Contains(currentCompanyId))
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    return;
                }
            }

            await _next(context);
        }
    }
