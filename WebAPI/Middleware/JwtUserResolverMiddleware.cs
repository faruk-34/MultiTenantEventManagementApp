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

        //  JWT ile gelen kullanıcı bilgilerinin uygulama genelinde kullanılabilmesi için bir context nesnesine (IWorkContext) aktarılmasını sağlar.
        //  Böylece servislerde tekrar token çözümleme ihtiyacı olmadan IWorkContext üzerinden kullanıcı bilgilerine erişim sağlanabilir.
        public async Task InvokeAsync(HttpContext context, IWorkContext workContext)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);  
                var tenantId = context.User.FindFirstValue("TenantId");  

                workContext.UserId = int.Parse(userId);
                workContext.TenantId = int.Parse(tenantId);
            }

            await _next(context);
        }
    }

}
