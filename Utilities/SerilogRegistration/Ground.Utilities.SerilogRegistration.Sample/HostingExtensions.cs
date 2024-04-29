using Ground.Extensions.DependencyInjection;
using Serilog;

namespace Ground.Utilities.SerilogRegistration.Sample
{
    public static class HostingExtensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddGroundWebUserInfoService(c =>
            {
                c.DefaultUserId = "1";
            }, false);
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();            

            return builder.Build();
        }
        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            app.UseSerilogRequestLogging();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();


            return app;
        }
    }
}
