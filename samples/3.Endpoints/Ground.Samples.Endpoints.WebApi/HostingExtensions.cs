using Ground.Endpoints.WebApi.Extensions.DependencyInjection;
using Ground.Endpoints.WebApi.Extensions.ModelBinding;
using Ground.Extensions.DependencyInjection;
using Ground.Infra.Data.Sql.Commands.Interceptors;
using Ground.Samples.Infra.Data.Sql.Commands.Common;
using Ground.Samples.Infra.Data.Sql.Queries.Common;
using Microsoft.EntityFrameworkCore;

namespace Ground.Samples.Endpoints.WebApi
{
    /// <summary>
    /// this class is used for injecting dependencies
    /// </summary>
    public static class HostingExtensions
    {
        /// <summary>
        /// Dependencies
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            string conn = "Server=.; Initial Catalog=GroundSample; User Id=sa; Password=P@ssw0rd;encrypt=false";
           
            builder.Services.AddGroundTraniTranslator(c =>
            {
                c.ConnectionString = conn;
                c.AutoCreateSqlTable = true;
                c.SchemaName = "dbo";
                c.TableName = "TraniTranslations";
                c.ReloadDataIntervalInMinuts = 1;
            });

            // Required for auditing (provides UserIdOrDefault(), IP, agent, etc.). True = Fake user info.
            builder.Services.AddGroundWebUserInfoService(builder.Configuration,true);

            builder.Services.AddGroundAutoMapperProfiles(option =>
            {
                option.AssmblyNamesForLoadProfiles = "Ground.Samples";
            });            

            builder.Services.AddGroundNewtonSoftSerializer();

            builder.Services.AddGroundInMemoryCaching();

            // Recommended approach: Interceptor-based auditing
            builder.Services.AddScoped<AddAuditDataInterceptor>();
            // 2) Attach interceptor to DbContext options via AddInterceptors
            builder.Services.AddDbContext<SampleCommandDbContext>((sp, options) =>
            {
                options.UseSqlServer(conn);
                options.AddInterceptors(sp.GetRequiredService<AddAuditDataInterceptor>());
            });

            builder.Services.AddDbContext<SampleQueryDbContext>(c => c.UseSqlServer(conn));

            builder.Services.AddGroundApiCore("Ground");

            builder.Services.AddNonValidatingValidator();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();

            return builder.Build();
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            app.UseGroundApiExceptionHandler();
            
            //app.UseSerilogRequestLogging();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();


            return app;
        }
    }
}
