using Microsoft.EntityFrameworkCore;

namespace Ground.Utilities.OpenTelemetryRegistration.Sample.Models
{
    public class PersonContext : DbContext
    {
        public PersonContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Person> People { get; set; }

    }
}
