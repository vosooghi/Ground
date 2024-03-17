using Microsoft.EntityFrameworkCore;

namespace Ground.Utilities.OpenTelemetryRegistration.Sample.Models
{
    public static class SeedData
    {

        public static void EnsurePopulate(IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<PersonContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (!context.People.Any())
            {
                context.People.AddRange( new Person []{

                    new Person { FirstName = "Abbas", LastName = "Vosoughi" },
                    new Person { FirstName = "Radmehr", LastName = "Vosoughi" },
                });
                context.SaveChanges();
            }
        }
    }
}
