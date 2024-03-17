using Microsoft.EntityFrameworkCore;

namespace Ground.Extensions.UsersManagement.SampleMvcCoreIdentity.Data
{
    public class AppIdentityDbContext : DbContext
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {
        }
    }
}
