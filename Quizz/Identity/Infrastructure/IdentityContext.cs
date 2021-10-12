using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure
{
    public class IdentityContext : DbContext
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        { }
    }
}
