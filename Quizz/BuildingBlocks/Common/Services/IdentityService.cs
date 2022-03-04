using Microsoft.AspNetCore.Http;

namespace Quizz.Common.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IHttpContextAccessor httpContext;

        public IdentityService(IHttpContextAccessor httpContext)
        {
            this.httpContext = httpContext;
        }

        public string GetUserIdentity()
        {
            return httpContext.HttpContext.User.FindFirst("sub").Value;
        }
    }
}