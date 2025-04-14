using Application.Models;
using Infrastructure;
using System.Security.Claims;

namespace WebAPI.Middleware
{
    public class JwtUserResolverMiddleware
    {
        private readonly RequestDelegate _next;
        public JwtUserResolverMiddleware(RequestDelegate next)
        {
            _next = next;
           
        }

        public async Task InvokeAsync(HttpContext context, WorkContext workContext)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var tenantId = context.User.FindFirstValue("tenantId");
                // buradan user bilgilerini DB’den çekip context.Items gibi yerlere atayabilirsin.

                workContext.UserId = int.Parse(userId);
                workContext.TenantId = int.Parse(tenantId);
            }

            await _next(context);
        }
    }

}
