using Ground.Endpoints.WebApi.Extentions.DependencyInjection;
using Ground.Samples.Infra.Data.Sql.Commands.Common;
using Ground.Samples.Infra.Data.Sql.Queries.Common;
using Microsoft.EntityFrameworkCore;
using Ground.Extensions.DependencyInjection;
using Ground.Endpoints.WebApi.Extentions.ModelBinding;

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

            builder.Services.AddGroundWebUserInfoService(builder.Configuration,true);//fake version

            builder.Services.AddGroundAutoMapperProfiles(option =>
            {
                option.AssmblyNamesForLoadProfiles = "Ground.Samples";
            });

            builder.Services.AddNonValidatingValidator();

            builder.Services.AddGroundNewtonSoftSerializer();

            builder.Services.AddGroundInMemoryCaching();

            builder.Services.AddDbContext<SampleCommandDbContext>(c => c.UseSqlServer(conn));
            builder.Services.AddDbContext<SampleQueryDbContext>(c => c.UseSqlServer(conn));

            builder.Services.AddGroundApiCore("Ground");//= AddControllers(); FluentValidation();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            return builder.Build();
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            app.UseGroundApiExceptionHandler();//missing
            
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
